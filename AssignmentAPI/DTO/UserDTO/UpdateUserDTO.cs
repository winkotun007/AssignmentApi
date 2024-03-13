using System;
using FluentValidation;
using System.ComponentModel.DataAnnotations;

namespace AssignmentAPI.DTO.UserDTO
{
    public class UpdateUserDTO
    {
        [StringLength(255)]
        public string UserId { get; set; } = null!;

        [StringLength(255)]
        public string UserName { get; set; } = null!;

        [StringLength(255)]
        public string FristName { get; set; } = null!;

        [StringLength(255)]
        public string LastName { get; set; } = null!;

        [StringLength(255)]
        public string Password { get; set; } = null!;
    }

    public class UserUpdateDTOValidator : AbstractValidator<UpdateUserDTO>
    {
        public UserUpdateDTOValidator()
        {
            RuleFor(x => x.UserId).NotNull().NotEmpty().WithMessage("First Name is required.");
            RuleFor(x => x.FristName).NotNull().NotEmpty().WithMessage("First Name is required.");
            RuleFor(x => x.LastName).NotNull().NotEmpty().WithMessage("Last Name is required.");
            RuleFor(x => x.UserName).NotNull().NotEmpty().WithMessage("User Name is required.");
            RuleFor(x => x.Password).NotNull().NotEmpty().WithMessage("Password is required.");
        }
    }
    }

