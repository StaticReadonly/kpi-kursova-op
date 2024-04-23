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

        public TenderSearchViewModel GetTenders(TenderSearchModel searchModel)
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
    }
}
