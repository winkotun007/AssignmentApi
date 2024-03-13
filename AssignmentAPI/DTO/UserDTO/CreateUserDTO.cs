using System;
using System.ComponentModel.DataAnnotations;
using AssignmentAPI.DTO.VisitorsDTO;
using FluentValidation;

namespace AssignmentAPI.DTO.UserDTO
{
	public class CreateUserDTO
	{
        [StringLength(255)]
        public string UserName { get; set; } = null!;

        [StringLength(255)]
        public string FristName { get; set; } = null!;

        [StringLength(255)]
        public string LastName { get; set; } = null!;

        [StringLength(255)]
        public string Password { get; set; } = null!;
    }

    public class UserCreateDTOValidator : AbstractValidator<CreateUserDTO>
    {
        public UserCreateDTOValidator()
        {
            RuleFor(x => x.FristName).NotNull().NotEmpty().WithMessage("First Name is required.");
            RuleFor(x => x.LastName).NotNull().NotEmpty().WithMessage("Last Name is required.");
            RuleFor(x => x.UserName).NotNull().NotEmpty().WithMessage("User Name is required.");
            RuleFor(x => x.Password).NotNull().NotEmpty().WithMessage("Password is required.");
        }
    }
}

