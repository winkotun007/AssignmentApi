using System;
namespace AssignmentAPI.Models
{
	public class BaseModel
	{
		public DateTime? CreatedDate { get; set; } = DateTime.UtcNow;

		public DateTime? UpdatedDate { get; set; } = DateTime.UtcNow;

		public string? CreatedBy { get; set; } = "admin";

		public string? UpdatedBy { get; set; } = "admin";
	}
}

