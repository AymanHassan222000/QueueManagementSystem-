using System.ComponentModel.DataAnnotations;

namespace QMS.BL.DTOs.SubscriptionDTOs;

public class SubscriptionBaseDTO
{
    public DateTime? SubscriptionDate { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    [Required]
    public SubscriptionStatus Status { get; set; }

    [MaxLength(50)]
    public string PaymentMethod { get; set; }

    public bool AutoRenewal { get; set; }

    [Range(0, double.MaxValue, ErrorMessage = "Amount must be a positive number.")]
    [DataType(DataType.Currency)]
    public decimal Amount { get; set; }

    [StringLength(3)]
    [RegularExpression(@"^[A-Z]{3}$", ErrorMessage = "Currency must be a 3-letter ISO code.")]
    public string Currency { get; set; }

    [StringLength(100)]
    public string? DiscountCode { get; set; }

    public string? Notes { get; set; }

    public DateTime? LastPaymentDate { get; set; }

    public int CompanyId { get; set; }

}
