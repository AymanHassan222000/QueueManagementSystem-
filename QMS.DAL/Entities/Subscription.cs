using QMS.DAL.Entities.EntityBase;
using System.ComponentModel.DataAnnotations;

namespace QMS.DAL.Models;

public class Subscription : Entity
{
	public int SubscriptionID { get; set; }

	public DateTime SubscriptionDate { get; set; } = DateTime.Now;

	public DateTime StartDate { get; set; } = DateTime.Now;

	public DateTime LastPaymentDate { get; set; } = DateTime.Now;

	public DateTime? EndDate { get; set; }

	[Required]
	public SubscriptionStatus  Status { get; set; } 

	[Required]
	[MaxLength(50)]
	public string PaymentMethod { get; set; } 

	public bool AutoRenewal { get; set; }

	public decimal Amount { get; set; }

	[Required]
	[MaxLength(10)]
	public string Currency { get; set; }

	[MaxLength(100)]
	public string? DiscountCode { get; set; }

	public string? Notes { get; set; }


	[Required]
	public int CompanyID { get; set; }
	public Company Company { get; set; }
}
