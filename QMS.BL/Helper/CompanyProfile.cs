using QMS.BL.DTOs.CompanyDTOs;

namespace QMS.API.Helper;

public class CompanyProfile : Profile
{
    public CompanyProfile()
    {
        CreateMap<Company, CompanyDTO>()
            .ForMember(dest => dest.BranchIDs,opt => opt.MapFrom(src => src.Branchs.Select(b => b.BranchID).ToList()))
            .ForMember(dest => dest.BranchNames,opt => opt.MapFrom(src => src.Branchs.Select(b => b.Name).ToList()))
            .ForMember(dest => dest.SubscriptionIDs,opt => opt.MapFrom(src => src.Subscriptions.Select(b => b.SubscriptionID).ToList()));

        CreateMap<CreateCompanyDTO, Company>();

        CreateMap<UpdateCompanyDTO, Company>()
            .ForMember(dest => dest.ModifiedOn,opt => opt.MapFrom(_=> DateTime.UtcNow));
    }
}
