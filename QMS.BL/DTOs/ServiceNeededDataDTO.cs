using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QMS.DAL.DTOs
{
	public class ServiceNeededDataDTO
	{
		[MaxLength(100)]
		public required string DataType { get; set; }

		[MaxLength(100)]
		public required string TitleData { get; set; }

		public DateTime? CreatedOn { get; set; }

		[MaxLength(100)]
		public required string CreatedBy { get; set; }

		public DateTime? ModifiedOn { get; set; }

		[MaxLength(100)]
		public required string ModifiedBy { get; set; }

		public byte ServiceId { get; set; }

		public bool? IsDeleted { get; set; }

	}
}
