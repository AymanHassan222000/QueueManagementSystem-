using QMS.BL.DTOs.IdentityTypesDTOs;

namespace QMS.BL.Helper;

public class IdentityTypeProfile:Profile
{
    public IdentityTypeProfile()
    {
        CreateMap<IdentityTypeRequestDTO, IdentityType>().
            ForMember(dest => dest.ModifiedOn, opt => opt.MapFrom(_=> DateTime.UtcNow));

        CreateMap<IdentityType, IdentityTypeResponseDTO>();
    }
}
