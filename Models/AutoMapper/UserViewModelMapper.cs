using AutoMapper;
using Models.DbModels;
using Models.ViewModels.User;

namespace Models.AutoMapper
{
    public class UserViewModelMapper : Profile
    {
        public UserViewModelMapper() 
        {
            CreateMap<UserModel, UserViewModel>();
        }
    }
}
