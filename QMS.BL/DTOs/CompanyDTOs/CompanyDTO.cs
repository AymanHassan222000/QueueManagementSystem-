namespace QMS.BL.DTOs.CompanyDTOs;

public class CompanyDTO : CompanyBaseDTO
{
    public int CompanyID { get; set; }
    public DateTime CreatedOn { get; set; } 
    public string CreatedBy { get; set; }
    public DateTime ModifiedOn { get; set; } 
    public string ModifiedBy { get; set; }
    public bool IsDeleted { get; set; }
    public List<int> BranchIDs { get; set; } = new();
    public List<string> BranchNames { get; set; } = new();
    public List<int> SubscriptionIDs { get; set; } = new();
}
