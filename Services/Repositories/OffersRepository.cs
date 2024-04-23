using Kursova.DatabaseContext;
using Services.Abstractions;

namespace Services.Repositories
{
    public class OffersRepository : IOffersRepository
    {
        private readonly DatabaseContext _context;

        public OffersRepository(DatabaseContext context)
        {
            _context = context;
        }
    }
}
