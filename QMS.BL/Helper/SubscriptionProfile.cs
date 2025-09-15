using QMS.BL.DTOs.SubscriptionDTOs;

namespace QMS.API.Helper;

public class SubscriptionProfile : Profile
{
    public SubscriptionProfile()
    {
        CreateMap<Subscription,SubscriptionDTO>();
        CreateMap<CreateSubscriptionDTO, Subscription>();

        CreateMap<UpdateSubscriptionDTO, Subscription>()
            .ForMember(dest => dest.ModifiedOn, opt => opt.MapFrom(_ => DateTime.UtcNow));
    }
}
