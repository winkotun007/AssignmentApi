using System;
using FluentValidation;
using System.ComponentModel.DataAnnotations;

namespace AssignmentAPI.DTO.UserDTO
{

    public class LoginUserDTO
    {
        [StringLength(255)]
        public string? UserName { get; set; } = null!;

        [StringLength(255)]
        public string? Password { get; set; } = null!;
    }


    public class UserLoginDTOValidator : AbstractValidator<LoginUserDTO>
    {
        public UserLoginDTOValidator()
        {
            RuleFor(x => x.UserName).NotNull().NotEmpty().WithMessage("User Name is required.");
            RuleFor(x => x.Password).NotNull().NotEmpty().WithMessage("Password is required.");
        }
    }

}
