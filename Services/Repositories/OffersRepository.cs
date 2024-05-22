using AutoMapper;
using Kursova.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Models.ControllerModels;
using Models.DbModels;
using Models.Exceptions;
using Models.Options;
using Models.ViewModels.UserOffers;
using Models.ViewModels.UserTender;
using Services.Abstractions;

namespace Services.Repositories
{
    public class OffersRepository : IOffersRepository
    {
        private readonly ITenderStateInfoService _stateInfoService;
        private readonly DatabaseContext _context;
        private readonly PaginationOptions _paginationOptions;
        private readonly IMapper _mapper;

        public OffersRepository(
            DatabaseContext context,
            IOptions<PaginationOptions> paginationOptions,
            IMapper mapper,
            ITenderStateInfoService stateInfoService)
        {
            _paginationOptions = paginationOptions.Value;
            _context = context;
            _mapper = mapper;
            _stateInfoService = stateInfoService;
        }

        public async Task OfferAction(Guid guid, OfferActionModel model)
        {
            var tenderCheck = await _context.TenderModels
                .Where(t => t.Id == model.TenderId)
                .Where(t => t.OwnerId == guid)
                .Include(t => t.Offers)
                .FirstOrDefaultAsync();

            if (tenderCheck == null)
                throw new ArgumentException("Can't access tender for specified user");

            if (!_stateInfoService.IsWaitingForOffers(tenderCheck))
                throw new ArgumentException("Tender isn't accepting offers");

            switch (model.Action)
            {
                case "accept":
                    {
                        await AcceptOffer(tenderCheck, model);
                        tenderCheck.StateId = 2;
                        break;
                    }
                case "refuse":
                    {
                        await RefuseOffer(tenderCheck, model);
                        break;
                    }
                default:
                    throw new ArgumentException("Invalid offer action");
            }

            await _context.SaveChangesAsync();
        }

        private Task AcceptOffer(TenderModel tmodel, OfferActionModel model)
        {
            Guid executerId = Guid.Empty;

            tmodel.Offers.ForEach(m =>
            {
                if (m.Id == model.OfferId)
                {
                    m.StateId = 3;
                    executerId = m.OffererId;
                }
                else
                {
                    m.StateId = 2;
                }
            });

            tmodel.ExecuterId = executerId;

            return Task.CompletedTask;
        }

        private Task RefuseOffer(TenderModel tmodel, OfferActionModel model)
        {
            var offer = tmodel.Offers.Where(o => o.Id == model.OfferId)
                .FirstOrDefault();

            if (offer == null)
                throw new ArgumentException("Offer does not exist");

            offer.StateId = 2;

            return Task.CompletedTask;
        }

        public UserOffersViewModel GetUserOffers(UserOffersModel model, Guid userId)
        {
            UserOffersViewModel res = new UserOffersViewModel()
            {
                CurrentPage = model.Page
            };

            List<OfferModel>? offers = null;

            offers = _context.OfferModels
                .Where(m => m.OffererId == userId)
                .Include(m => m.State)
                .OrderByDescending(m => m.CreationDate)
                .ToList();

            var cnt = offers.Count;

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

            if (model.Page > res.TotalPages)
                throw new ArgumentException($"Tried to access unexisting page {model.Page}");

            res.Offers = offers.Skip((model.Page - 1) * _paginationOptions.ItemsPerPage)
                .Take(_paginationOptions.ItemsPerPage)
                .ToList();

            return res;
        }

        public UserTenderOffersViewModel GetTenderOffers(UserTenderOffersModel model, Guid owner)
        {
            var user = _context.Users.Where(u => u.Id == owner)
                .Include(u => u.Tenders)
                .FirstOrDefault();

            if (user == null)
                throw new ArgumentException($"No user with id {owner} was found");

            if (!user.Tenders.Exists(t => t.Id == model.Id))
                throw new ArgumentException($"Can't find tender with id {model.Id} for user {owner}");

            UserTenderOffersViewModel result = new UserTenderOffersViewModel();

            result.Id = model.Id;
            result.CurrentPage = model.Page;

            var offers = _context.OfferModels
                .Where(o => o.TenderId == model.Id)
                .Include(o => o.Offerer)
                .Include(o => o.State)
                .ToList();

            if (offers.Count % _paginationOptions.ItemsPerPage != 0)
            {
                result.TotalPages = offers.Count / _paginationOptions.ItemsPerPage + 1;
            }
            else
            {
                result.TotalPages = offers.Count / _paginationOptions.ItemsPerPage;
            }

            if (result.TotalPages == 0)
            {
                return result;
            }

            if (result.CurrentPage > result.TotalPages)
                throw new ArgumentException($"Tried to access unexisting page {result.CurrentPage}");

            result.Offers = _mapper.Map<List<UserTenderOfferViewModel>>(offers
                .Skip((result.CurrentPage - 1) * _paginationOptions.ItemsPerPage)
                .Take(_paginationOptions.ItemsPerPage)
                .ToList());

            return result;
        }

        public async Task NewOffer(NewOfferModel model)
        {
            var tender = _context.TenderModels.FirstOrDefault(t => t.Id == model.TenderId);

            if (tender == null)
                throw new FormFieldException("Description", "Can't create offer for unexcisting tender");

            if (_stateInfoService.IsCreated(tender))
                throw new Exception("Tender isn't started yet");

            if (tender.OwnerId == model.OwnerId)
                throw new FormFieldException("Description", "Can't create offer for own tender");

            OfferModel newModel = new OfferModel()
            {
                OffererId = model.OwnerId,
                CreationDate = DateTime.UtcNow,
                StateId = 1,
                Price = model.Price,
                TenderId = tender.Id,
                Description = model.Description
            };

            await _context.OfferModels.AddAsync(newModel);
            await _context.SaveChangesAsync();
        }
    }
}
