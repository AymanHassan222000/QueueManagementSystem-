namespace QMS.BL.DTOs.BranchDTOs;

public class BranchDTO : BranchBaseDTO
{
    public int BranchID { get; set; }
    public DateTime CreatedOn { get; set; }
    public string CreatedBy { get; set; }
    public DateTime ModifiedOn { get; set; }
    public string ModifiedBy { get; set; }
    public bool IsDeleted { get; set; }

    public List<int> QueueIDs { get; set; } = new();

    public List<string> QueueNames { get; set; } = new();

    public List<int> UserIDs { get; set; } = new();
}
