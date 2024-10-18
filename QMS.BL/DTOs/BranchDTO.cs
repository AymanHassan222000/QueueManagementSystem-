using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QMS.BL.DTOs
{
	public class BranchDTO
	{
		[MaxLength(100)]
		public required string Name { get; set; }

		[MaxLength(20), RegularExpression(@"^\d+$")]
		public required string Phone { get; set; }

		[MaxLength(50)]
		public required string Street { get; set; }

		[MaxLength(50)]
		public required string City { get; set; }

		[MaxLength(50)]
		public string? State { get; set; }

		[MaxLength(50)]
		public required string Country { get; set; }

		[MaxLength(20)]
		public string? BranchCode { get; set; }

		public decimal Longitude { get; set; }

		public decimal Latitude { get; set; }

		public DateTime? CreatedOn { get; set; }

		[MaxLength(100)]
		public required string CreatedBy { get; set; }

		public DateTime? ModifiedOn { get; set; }

		[MaxLength(100)]
		public required string ModifiedBy { get; set; }

		public long CompanyId { get; set; }

	}
}
