using System;
using AssignmentAPI.DTO.RoomDTO;
using FluentValidation;

namespace AssignmentAPI.DTO.GuestAccessDTO
{
    public class CreateGuestAccessDTO
    {
        public string? Path { get; set; }
        public bool? isGetAccess { get; set; }
        public bool? isPostAccess { get; set; }
        public bool? isPutAccess { get; set; }
        public bool? isDeleteAccess { get; set; }
    }

    public class GuestAccessCreateDTOValidator : AbstractValidator<CreateGuestAccessDTO>
    {
        public GuestAccessCreateDTOValidator()
        {
            RuleFor(x => x.Path).NotNull().NotEmpty().MaximumLength(255).WithMessage("Path is required.");
            RuleFor(x => x.isGetAccess).NotNull().NotEmpty().WithMessage("GetAccess is required.");
            RuleFor(x => x.isPostAccess).NotNull().NotEmpty().WithMessage("PostAccess is required.");
            RuleFor(x => x.isPutAccess).NotNull().NotEmpty().WithMessage("PutAcess is required.");
            RuleFor(x => x.isDeleteAccess).NotNull().NotEmpty().WithMessage("DeleteAccess is required.");



        }
    }
}

