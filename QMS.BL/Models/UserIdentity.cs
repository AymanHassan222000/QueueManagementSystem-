using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QMS.BL.Models
{
	public class UserIdentity
	{
		public int Id { get; set; }

		public required string IdentityId { get; set; }

		public DateTime CreatedOn { get; set; } = DateTime.Now;

		public required string CreatedBy { get; set; }

		public DateTime ModifiedOn { get; set; } = DateTime.Now;

		public required string ModifiedBy { get; set; }

		public byte IdentityTypeId { get; set; }

		public long UserId { get; set; }

	}
}
