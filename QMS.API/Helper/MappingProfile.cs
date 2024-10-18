using AutoMapper;
using QMS.BL.DTOs;
using QMS.BL.Models;

namespace QMS.API.Helper
{
	public class MappingProfile : Profile
	{
		public MappingProfile()
		{
			CreateMap<Company, CompanyDTO>().ReverseMap();

			CreateMap<Subscription, SubscriptionDTO>().ReverseMap();

			CreateMap<Branch, BranchDTO>().ReverseMap();

			CreateMap<Feedback, FeedbackDTO>().ReverseMap();

			CreateMap<IdentityType, IdentityTypeDTO>().ReverseMap();

			CreateMap<Queue, QueueDTO>().ReverseMap();

			CreateMap<Role, RoleDTO>().ReverseMap();

			CreateMap<ServiceNeededData, ServiceNeededDataDTO>().ReverseMap();

			CreateMap<Service, ServiceDTO>().ReverseMap();

			CreateMap<UserIdentity, UserIdentityDTO>().ReverseMap();

			CreateMap<UserQueueNeededData, UserQueueNeededDataDTO>().ReverseMap();

			CreateMap<UserQueue, UserQueueDTO>().ReverseMap();

			CreateMap<User, UserDTO>().ReverseMap();
		}

	}
}
