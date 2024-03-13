using System;
namespace AssignmentAPI.Models
{
	public class GuestAccessModel : BaseModel
    {
        public string GuestAccessId { get; set; } = Guid.NewGuid().ToString();
        public string? Path { get; set; }
        public bool? isGetAccess { get; set; }
        public bool? isPostAccess { get; set; }
        public bool? isPutAccess { get; set; }
        public bool? isDeleteAccess { get; set; }
    }
}

