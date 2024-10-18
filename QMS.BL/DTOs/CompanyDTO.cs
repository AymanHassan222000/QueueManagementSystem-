using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QMS.BL.DTOs
{
	public class CompanyDTO
	{
		public required string Name { get; set; }

		[MaxLength(100)]
		public required string Email { get; set; }

		[DisplayName("Created On")]
		public DateTime? CreatedOn { get; set; }

		[DisplayName("Created By"),MaxLength(100)]
		public required string CreatedBy { get; set; }

		[DisplayName("Modified On")]
		public DateTime? ModifiedOn { get; set; }

		[DisplayName("Modified By"),MaxLength(100)]
		public required string ModifiedBy { get; set; }

		public bool? IsDeleted { get; set; }

	}
}
