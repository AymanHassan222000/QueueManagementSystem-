using QMS.BL.DTOs.QueueDTOs;

namespace QMS.API.Helper;

public class QueueProfile : Profile
{
    public QueueProfile()
    {
        CreateMap<QueueRequestDTO, Queue>()
            .ForMember(dest => dest.ModifiedOn, opt => opt.MapFrom(_ => DateTime.Now));

        CreateMap<Queue, QueueResponseDTO>();
    }

}
