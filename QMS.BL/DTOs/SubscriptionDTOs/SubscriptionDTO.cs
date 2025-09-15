namespace QMS.BL.DTOs.SubscriptionDTOs;

public class SubscriptionDTO : SubscriptionBaseDTO
{
    public DateTime CreatedOn { get; set; }
    public string CreatedBy { get; set; }
    public DateTime ModifiedOn { get; set; }
    public string ModifiedBy { get; set; }
    public bool IsDeleted { get; set; }
}
