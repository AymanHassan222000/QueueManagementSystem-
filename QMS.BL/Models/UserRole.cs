using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QMS.BL.Models
{
	public class UserRole
	{
		public long UserId { get; set; }

		public byte RoleId { get; set; }

		public long? BranchId { get; set; }

		public long? CompanyId { get; set; }
		public DateTime CreatedOn { get; set; } = DateTime.Now;

		public string CreatedBy { get; set; }

		public DateTime ModifiedOn { get; set; } = DateTime.Now;

		public string ModifiedBy { get; set; }

	}
}
