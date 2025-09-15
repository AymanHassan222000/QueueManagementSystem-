using QMS.BL.DTOs.BranchDTOs;

namespace QMS.API.Helper;

public class BranchProfile : Profile
{
    public BranchProfile()
    {
        CreateMap<Branch, BranchDTO>()
            .ForMember(dest => dest.UserIDs,opt => opt.MapFrom(src => src.Users.Select(e => e.UserID).ToList()))
            .ForMember(dest => dest.QueueIDs,opt => opt.MapFrom(src => src.Queues.Select(e => e.QueueID).ToList()))
            .ForMember(dest => dest.QueueNames,opt => opt.MapFrom(src => src.Queues.Select(e => e.Name).ToList()));


        CreateMap<CreateBranchDTO, Branch>();

        CreateMap<UpdateBranchDTO, Branch>()
            .ForMember(dest => dest.ModifiedOn,opt => opt.MapFrom(_=> DateTime.UtcNow));
    }
}
