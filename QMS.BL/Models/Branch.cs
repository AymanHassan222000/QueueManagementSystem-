using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QMS.BL.Models
{
	public class Branch
	{
		public long Id { get; set; }

		public required string Name { get; set; }

		public required string Phone { get; set; }

		public required string Street { get; set; }

		public required string City { get; set; }

		public string? State { get; set; }

		public required string Country { get; set; }

		public required string? BranchCode { get; set; }

		public decimal Longitude { get; set; }

		public decimal Latitude { get; set; }

		public DateTime CreatedOn { get; set; } = DateTime.Now;

		public required string CreatedBy { get; set; }

		public DateTime ModifiedOn { get; set; } = DateTime.Now;

		public required string ModifiedBy { get; set; }

		public long CompanyId { get; set; }

	}
}
