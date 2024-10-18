using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QMS.BL.Models
{
	public class Subscription
	{
		public int Id { get; set; }

		public DateTime SubscriptionDate { get; set; } = DateTime.Now;

		public DateTime StartDate { get; set; } = DateTime.Now;

		public DateTime? EndDate { get; set; }

		[MaxLength(50)]
		public string Status { get; set; }

		[MaxLength(50)]
		public string PaymentMethod { get; set; }

		public bool AutoRenewal { get; set; }

		public decimal Amount { get; set; }

		[MaxLength(10)]
		public string Currency { get; set; }

		[MaxLength(100)]
		public string? DiscountCode { get; set; }

		public string? Notes { get; set; }

		public DateTime LastPaymentDate { get; set; } = DateTime.Now;

		public bool IsDeleted { get; set; }

		public DateTime CreatedOn { get; set; } = DateTime.Now;

		[MaxLength(100)]
		public required string CreatedBy { get; set; }

		public DateTime ModifiedOn { get; set; } = DateTime.Now;

		[MaxLength(100)]
		public required string ModifiedBy { get; set; }

		public long CompanyId { get; set; }
	}
}
