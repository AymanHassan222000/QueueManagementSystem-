using QMS.BL.DTOs.UserDTOs;

namespace QMS.BL.Helper
{
    public class UserProfile : Profile
    {
        private readonly IUnitOfWork _unitOfWork;
        public UserProfile(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public UserProfile()
        {
            CreateMap<User, UserDTO>();

            CreateMap<CreateUserDTO, User>();
                //.ForMember(dest => dest.UserRoles, opt => opt.MapFrom(src =>
                //    new List<UserRole>
                //    {
                //        new UserRole
                //        {
                //            BranchID = src.BranchID,
                //            RoleID = 5,
                //            CreatedBy = "System",
                //            ModifiedBy = "System",
                //        }
                //    }
                //));


            CreateMap<UpdateUserDTO, User>()
                .ForMember(dest => dest.ModifiedOn, opt => opt.MapFrom(_ => DateTime.UtcNow));
        }

    }
}
