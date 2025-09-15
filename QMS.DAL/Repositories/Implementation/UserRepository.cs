using QMS.DAL.Models;

namespace QMS.DAL.Repositories;

//public class UserRepository : BaseRepository<User>
//{
//	public UserRepository(ApplicationDbContext context) : base(context)
//	{
//	}

//	public async Task<List<string>> GetRoles(User user)
//	{
//		var roles = _context.Set<User>().Join(
//			_context.Set<UserRole>(),
//			u => u.Id,
//			ur => ur.UserId,
//			(u, ur) => new
//			{
//				UserId = u.Id,
//				RoleId = ur.RoleId
//			}
//		)
//		.Join(
//			_context.Set<Role>(),
//			u => u.RoleId,
//			r => r.Id,
//			(u, r) => new
//			{
//				u.UserId,
//				Role = r.Name
//			}
//		)
//		.Where(m => m.UserId == user.Id).Select(m => m.Role).ToList();

//		return roles;
//	}

//}
