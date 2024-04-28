using AutoMapper;
using Models.DbModels;
using Models.ViewModels;

namespace Models.AutoMapper
{
    public class UserTenderOfferViewModelMapper : Profile
    {
        public UserTenderOfferViewModelMapper()
        {
            CreateMap<OfferModel, UserTenderOfferViewModel>()
                .ForMember(m => m.OffererName, a => a.MapFrom(m => m.Offerer.Name))
                .ForMember(m => m.OffererSurname, a => a.MapFrom(m => m.Offerer.Surname))
                .ForMember(m => m.OffererPatronimyc, a => a.MapFrom(m => m.Offerer.Patronimyc));
        }
    }
}
