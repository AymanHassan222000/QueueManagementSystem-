using QMS.DAL.Entities.EntityBase;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QMS.DAL.Models;

public class UserQueue : Entity
{
    [Key]
    public int UserQueueID { get; set; }

    [Required]
    public int UserID { get; set; }

    [Required]
    public int QueueID { get; set; }

    [Required]
    public int ServiceID { get; set; }

    [Required]
    public DateTime StartedIn { get; set; } = DateTime.Now;

    public DateTime? EndedIn { get; set; }


    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public int? TimeTaken { get; private set; }

    //Navigation Properties
    public User User { get; set; }
    public Queue Queue { get; set; }
    public Service Service { get; set; }
    public IEnumerable<Feedback> Feedbacks { get; set; } = new HashSet<Feedback>();
    public IEnumerable<UserQueueNeededData> UserQueueNeededData { get; set; } = new HashSet<UserQueueNeededData>();
}
