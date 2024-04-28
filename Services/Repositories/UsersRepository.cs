using AutoMapper;
using Kursova.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using Models.ControllerModels;
using Models.DbModels;
using Models.Exceptions;
using Services.Abstractions;

namespace Services.Repositories
{
    public class UsersRepository : IUsersRepository
    {
        private readonly DatabaseContext _context;
        private readonly IMapper _mapper;

        public UsersRepository(
            DatabaseContext context, 
            IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Guid> CreateUser(UserRegistrationModel model)
        {
            UserModel user = _mapper.Map<UserModel>(model);
            _context.Users.Add(user);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch(DbUpdateException)
            {
                throw new FormFieldException("Email", "Email already exists");
            }

            return user.Id;
        }

        public async Task<UserModel?> GetUserDataByEmail(string email)
        {
            return await _context.Users.Where(u => u.Email == email).FirstOrDefaultAsync();
        }

        public async Task<UserModel?> GetUserDataById(string guid)
        {
            return await _context.Users.Where(u => u.Id == Guid.Parse(guid)).FirstOrDefaultAsync();
        }
    }
}
