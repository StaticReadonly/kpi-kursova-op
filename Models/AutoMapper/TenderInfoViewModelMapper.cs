using AutoMapper;
using Models.DbModels;
using Models.ViewModels.TenderSearch;

namespace Models.AutoMapper
{
    public class TenderInfoViewModelMapper : Profile
    {
        public TenderInfoViewModelMapper() 
        {
            CreateMap<TenderModel, TenderInfoViewModel>()
                .ForMember(d => d.State, cfg => cfg.MapFrom(s => s.State.Name))
                .ForMember(d => d.OwnerFullName, cfg => cfg.MapFrom(s => 
                    $"{s.Owner.Surname} {s.Owner.Name} {s.Owner.Patronimyc}"))
                .ForMember(d => d.ExecuterFullName, cfg => cfg.MapFrom(s =>
                    (s.Executer == null) ? (null) : ($"{s.Executer.Surname} {s.Executer.Name} {s.Executer.Patronimyc}")));
        }
    }
}
