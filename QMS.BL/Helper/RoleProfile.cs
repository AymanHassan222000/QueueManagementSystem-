using QMS.BL.DTOs.CompanyDTOs;
using QMS.BL.DTOs.RoleDTOs;

namespace QMS.API.Helper;

public class RoleProfile : Profile
{
    public RoleProfile()
    {
        CreateMap<Role, RoleDTO>();

        CreateMap<CreateRoleDTO, Role>();

        CreateMap<UpdateRoleDTO, Role>()
            .ForMember(dest => dest.ModifiedOn, opt => opt.MapFrom(_ => DateTime.UtcNow));
    }
}
