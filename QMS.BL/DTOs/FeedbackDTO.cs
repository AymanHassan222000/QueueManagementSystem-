﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QMS.BL.DTOs
{
	public class FeedbackDTO
	{
		public required string Comment { get; set; }

		public decimal? RateValue { get; set; }

		public DateTime? CreatedOn { get; set; }

		[MaxLength(100)]
		public required string CreatedBy { get; set; }

		public DateTime? ModifiedOn { get; set; }

		[MaxLength(100)]
		public required string ModifiedBy { get; set; }

		public int UserQueueId { get; set; }

	}
}
