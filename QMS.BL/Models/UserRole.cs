using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

		[MaxLength(100)]
		public string CreatedBy { get; set; }

		public DateTime ModifiedOn { get; set; } = DateTime.Now;

		[MaxLength(100)]
		public string ModifiedBy { get; set; }

		public bool IsDeleted { get; set; }

	}
}
