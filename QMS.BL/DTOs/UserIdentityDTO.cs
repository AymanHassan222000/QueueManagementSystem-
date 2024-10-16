using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QMS.BL.DTOs
{
	public class UserIdentityDTO
	{
		public required string IdentityId { get; set; }

		public DateTime? CreatedOn { get; set; }

		public required string CreatedBy { get; set; }

		public DateTime? ModifiedOn { get; set; }

		public required string ModifiedBy { get; set; }

		public byte IdentityTypeId { get; set; }

		public long UserId { get; set; }


	}
}
