using Kursova.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Models.ControllerModels;
using Models.DbModels;
using Models.Options;
using Models.ViewModels;
using Services.Abstractions;

namespace Services.Repositories
{
    public class TendersRepository : ITendersRepository
    {
        private readonly DatabaseContext _context;
        private readonly PaginationOptions _paginationOptions;

        public TendersRepository(
            DatabaseContext context,
            IOptions<PaginationOptions> options)
        {
            _context = context;
            _paginationOptions = options.Value;
        }

        public TenderSearchViewModel GetTenders(TenderSearchModel searchModel, Guid? userId)
        {
            if (searchModel.Page < 1)
                throw new ArgumentException("Can't access page below 1");

            TenderSearchViewModel result = new TenderSearchViewModel();
            result.Query = searchModel.Query;

            IQueryable<TenderModel> items = _context.TenderModels;

            if (result.Query != null)
            {
                items = items.Where(i => i.Name.Contains(result.Query));
            }

            if (userId != null)
                items = items.Where(t => t.OwnerId != userId);

            int amount = items.Count();

            result.CurrentPage = searchModel.Page;

            if (amount % _paginationOptions.ItemsPerPage != 0)
            {
                result.TotalPages = amount / _paginationOptions.ItemsPerPage + 1;
            }
            else
            {
                result.TotalPages = amount / _paginationOptions.ItemsPerPage;
            }

            if (result.CurrentPage > result.TotalPages)
                throw new ArgumentException("Specified page exceeds max pages");

            result.Tenders = items.Skip((searchModel.Page - 1) * _paginationOptions.ItemsPerPage)
                .Take(_paginationOptions.ItemsPerPage)
                .ToList();

            return result;
        }

        public TenderModel GetTenderInfo(Guid Id)
        {
            TenderModel? result = _context.TenderModels
                .Where(m => m.Id == Id)
                .Include(m => m.Owner)
                .Include(m => m.Offers)
                .Include(m => m.Executer)
                .Include(m => m.State)
                .FirstOrDefault();

            if (result == null)
                throw new ArgumentException($"Failed to find tender with id: {Id}");

            return result;
        }

        public UserTenderViewModel GetUserTenders(TenderSearchModel model, Guid userId)
        {
            UserTenderViewModel res = new UserTenderViewModel() 
            {
                CurrentPage = model.Page
            };

            List<TenderModel>? tenders = null;

            if (model.Query != null)
            {
                tenders = _context.TenderModels
                    .Where(t => t.OwnerId == userId && t.Name.Contains(model.Query))
                    .Include(t => t.State)
                    .OrderByDescending(t => t.CreationDate)
                    .ToList();
            }
            else
            {
                tenders = _context.TenderModels
                    .Where(t => t.OwnerId == userId)
                    .Include(t => t.State)
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

            if (model.Page > res.TotalPages)
                throw new ArgumentException($"Tried to access unexisting page {model.Page}");

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
                StateId = 1
            };

            _context.TenderModels.Add(newTender);
            await _context.SaveChangesAsync();
        }
    }
}
