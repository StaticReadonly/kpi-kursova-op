using AutoMapper;
using Models.DbModels;
using Models.ViewModels;

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
