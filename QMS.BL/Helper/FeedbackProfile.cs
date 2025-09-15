using QMS.BL.DTOs.FeedbackDTOs;

namespace QMS.API.Helper;

public class FeedbackProfile : Profile
{
    public FeedbackProfile()
    {
        CreateMap<FeedbackDTO, Feedback>()
            .ForMember(dest => dest.ModifiedOn ,opt => opt.MapFrom(_ => DateTime.UtcNow));
    }
}
