using Models.ControllerModels;
using Models.DbModels;

namespace Services.Abstractions
{
    public interface IUsersRepository
    {
        Task<UserModel?> GetUserDataByEmail(string email);

        Task<UserModel?> GetUserDataById(string guid);

        Task<Guid> CreateUser(UserRegistrationModel model);
    }
}