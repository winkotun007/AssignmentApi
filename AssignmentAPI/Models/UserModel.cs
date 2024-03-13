using System;
using System.ComponentModel.DataAnnotations;

namespace AssignmentAPI.Models
{
	public class UserModel : BaseModel
	{
        [StringLength(255)]
        public string UserId { get; set; } = Guid.NewGuid().ToString();

        [StringLength(255)]
        public string? UserName { get; set; } = null!;

        [StringLength(255)]
        public string? FristName { get; set; } = null!;

        [StringLength(255)]
        public string? LastName { get; set; } = null!;

        [StringLength(255)]
        public string? Password { get; set; } = null!;

        [StringLength(255)]
        public string? PasswordSalt { get; set; } = null!;
    }
}

