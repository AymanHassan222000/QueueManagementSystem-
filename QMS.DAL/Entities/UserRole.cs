using QMS.DAL.Entities.EntityBase;
using System.ComponentModel.DataAnnotations.Schema;

namespace QMS.DAL.Models;

public class UserRole : Entity
{
    public int UserID { get; set; }
	public User User { get; set; }

	public byte RoleID { get; set; }
	public Role Role { get; set; }

	public int? BranchID { get; set; }
	public Branch Branch { get; set; }

	public int? CompanyID { get; set; }
	public Company Company { get; set; }
}
