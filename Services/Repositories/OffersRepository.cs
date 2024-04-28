using AutoMapper;
using Kursova.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Models.ControllerModels;
using Models.Options;
using Models.ViewModels;
using Services.Abstractions;

namespace Services.Repositories
{
    public class OffersRepository : IOffersRepository
    {
        private readonly DatabaseContext _context;
        private readonly PaginationOptions _paginationOptions;
        private readonly IMapper _mapper;

        public OffersRepository(
            DatabaseContext context,
            IOptions<PaginationOptions> paginationOptions,
            IMapper mapper)
        {
            _paginationOptions = paginationOptions.Value;
            _context = context;
            _mapper = mapper;
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

            result.CurrentPage = model.Page;

            var offers = _context.OfferModels
                .Where(o => o.TenderId == model.Id)
                .Include(o => o.Offerer)
                .ToList();

            if (offers.Count % _paginationOptions.ItemsPerPage != 0)
            {
                result.TotalPages = offers.Count / _paginationOptions.ItemsPerPage + 1;
            }
            else
            {
                result.TotalPages = offers.Count / _paginationOptions.ItemsPerPage;
            }

            if (result.CurrentPage > result.TotalPages)
                throw new ArgumentException($"Tried to access unexisting page {result.CurrentPage}");

            result.Offers = _mapper.Map<List<UserTenderOfferViewModel>>(offers
                .Skip((result.CurrentPage - 1) * _paginationOptions.ItemsPerPage)
                .Take(_paginationOptions.ItemsPerPage)
                .ToList());

            return result;
        }
    }
}
