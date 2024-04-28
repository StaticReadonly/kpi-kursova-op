using AutoMapper;
using Models.ControllerModels;
using Models.DbModels;

namespace Models.AutoMapper
{
    public class UserModelMapper : Profile
    {
        public UserModelMapper()
        {
            CreateMap<UserRegistrationModel, UserModel>();
        }
    }
}
