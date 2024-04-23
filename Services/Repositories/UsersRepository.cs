using Kursova.DatabaseContext;
using Services.Abstractions;

namespace Services.Repositories
{
    public class UsersRepository : IUsersRepository
    {
        private readonly DatabaseContext _context;

        public UsersRepository(DatabaseContext context)
        {
            _context = context;
        }
    }
}
