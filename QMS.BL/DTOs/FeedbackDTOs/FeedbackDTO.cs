using System.ComponentModel.DataAnnotations;

namespace QMS.BL.DTOs.FeedbackDTOs;

public class FeedbackDTO
{
	[Required]
	public string Comment { get; set; } = null!;

	[Range(1,10, ErrorMessage = "Rate must be between 0 and 10.")]
	[DisplayFormat(DataFormatString = "0:0.00",ApplyFormatInEditMode = true)]
	public decimal? Rate { get; set; }

	public int UserQueueId { get; set; }
}
