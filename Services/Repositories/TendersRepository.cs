using Kursova.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Models.ControllerModels;
using Models.DbModels;
using Models.Exceptions;
using Models.Options;
using Models.ViewModels.TenderSearch;
using Models.ViewModels.UserTender;
using Services.Abstractions;
using Services.TenderStateInfoService;

namespace Services.Repositories
{
    public class TendersRepository : ITendersRepository
    {
        private readonly ITenderStateInfoService _stateInfoService;
        private readonly DatabaseContext _context;
        private readonly PaginationOptions _paginationOptions;

        public TendersRepository(
            DatabaseContext context,
            IOptions<PaginationOptions> options,
            ITenderStateInfoService stateInfoService)
        {
            _context = context;
            _paginationOptions = options.Value;
            _stateInfoService = stateInfoService;
        }

        public TenderSearchViewModel GetTenders(TenderSearchModel searchModel, Guid? userId)
        {
            if (searchModel.Page < 1)
                throw new ArgumentException("Can't access page below 1");

            TenderSearchViewModel result = new TenderSearchViewModel();
            result.Query = searchModel.Query;
            result.CurrentPage = searchModel.Page;

            IQueryable<TenderModel> items = _context.TenderModels.OrderByDescending(t => t.CreationDate);

            if (result.Query != null)
            {
                items = items.Where(i => i.Name.Contains(result.Query));
            }

            if (userId != null)
                items = items.Where(t => t.OwnerId != userId);

            items = items.WhereIsWaitingForOffers();

            int amount = items.Count();

            if (amount % _paginationOptions.ItemsPerPage != 0)
            {
                result.TotalPages = amount / _paginationOptions.ItemsPerPage + 1;
            }
            else
            {
                result.TotalPages = amount / _paginationOptions.ItemsPerPage;
            }

            if (result.TotalPages == 0)
            {
                return result;
            }

            if (result.CurrentPage > result.TotalPages)
                throw new ArgumentException("Specified page exceeds max pages");

            result.Tenders = items.Skip((searchModel.Page - 1) * _paginationOptions.ItemsPerPage)
                .Take(_paginationOptions.ItemsPerPage)
                .ToList();

            return result;
        }

        public TenderModel GetTenderInfo(Guid Id, Guid? userId)
        {
            TenderModel? result = _context.TenderModels
                .Include(m => m.Owner)
                .Include(m => m.Offers)
                .Include(m => m.Executer)
                .Include(m => m.State)
                .FirstOrDefault(m => m.Id == Id);

            if (result == null)
                throw new ArgumentException($"Failed to find tender with id: {Id}");

            if ((userId == null || userId != result.OwnerId) && !_stateInfoService.IsWaitingForOffers(result))
                throw new ArgumentException("Tender can't accept offers");

            return result;
        }

        public UserTenderViewModel GetUserTenders(TenderSearchModel model, Guid userId)
        {
            if (model.Page < 1)
                throw new ArgumentException("Can't access page below 1");

            UserTenderViewModel res = new UserTenderViewModel() 
            {
                Query = model.Query,
                CurrentPage = model.Page
            };

            List<TenderModel>? tenders = null;

            if (model.Query != null)
            {
                tenders = _context.TenderModels
                    .Where(t => t.OwnerId == userId && t.Name.Contains(model.Query))
                    .Include(t => t.State)
                    .Include(t => t.Executer)
                    .OrderByDescending(t => t.CreationDate)
                    .ToList();
            }
            else
            {
                tenders = _context.TenderModels
                    .Where(t => t.OwnerId == userId)
                    .Include(t => t.State)
                    .Include(t => t.Executer)
                    .OrderByDescending(t => t.CreationDate)
                    .ToList();
            }

            var cnt = tenders.Count;

            if (cnt % _paginationOptions.ItemsPerPage != 0)
            {
                res.TotalPages = cnt / _paginationOptions.ItemsPerPage + 1;
            }
            else
            {
                res.TotalPages = cnt / _paginationOptions.ItemsPerPage;
            }

            if (res.TotalPages == 0)
            {
                return res;
            }

            if (res.CurrentPage > res.TotalPages)
                throw new ArgumentException($"Tried to access unexisting page");

            res.Tenders = tenders.Skip((model.Page - 1) * _paginationOptions.ItemsPerPage)
                .Take(_paginationOptions.ItemsPerPage)
                .ToList();

            return res;
        }

        public async Task CreateNewTender(NewTenderModel model, Guid user)
        {
            TenderModel newTender = new TenderModel()
            {
                OwnerId = user,
                Name = model.Name,
                Description = model.Description,
                Cost = model.Cost,
                CreationDate = DateTime.UtcNow,
                StateId = 3
            };

            _context.TenderModels.Add(newTender);
            await _context.SaveChangesAsync();
        }

        public async Task TenderInitialAction(TenderInitialActionModel model, Guid user)
        {
            var tender = _context.TenderModels.FirstOrDefault(t => t.Id == model.TenderId);

            if (tender == null)
                throw new ArgumentException("Tender does not exist");

            if (tender.OwnerId != user)
                throw new ArgumentException("Access denied");

            if (!_stateInfoService.IsCreated(tender))
                throw new ArgumentException("Tender is already accepting offers");

            switch (model.Action)
            {
                case "Start":
                    {
                        tender.StateId = 1;
                        break;
                    }
                case "Delete":
                    {
                        _context.TenderModels.Remove(tender);
                        break;
                    }
                default:
                    {
                        throw new ArgumentException("Invalid action");
                    }
            }

            await _context.SaveChangesAsync();
        }
    }
}
