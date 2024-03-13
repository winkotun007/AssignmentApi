using System;
using AssignmentAPI.DTO.RoomDTO;
using FluentValidation;

namespace AssignmentAPI.DTO.GuestAccessDTO
{
    public class UpdateGuestAccessDTO
    {
        public string? GuestAccessId { get; set; }
        public string? Path { get; set; }
        public bool? isGetAccess { get; set; }
        public bool? isPostAccess { get; set; }
        public bool? isPutAccess { get; set; }
        public bool? isDeleteAccess { get; set; }
    }

    public class GuestAccessUpdateDTOValidator : AbstractValidator<UpdateGuestAccessDTO>
    {
        public GuestAccessUpdateDTOValidator()
        {
            RuleFor(x => x.GuestAccessId).NotNull().NotEmpty().MaximumLength(255).WithMessage("GuestAccessId is required");
            RuleFor(x => x.Path).NotNull().NotEmpty().MaximumLength(255).WithMessage("Path is required.");
            RuleFor(x => x.isGetAccess).NotNull().NotEmpty().WithMessage("GetAccess is required.");
            RuleFor(x => x.isPostAccess).NotNull().NotEmpty().WithMessage("PostAccess is required.");
            RuleFor(x => x.isPutAccess).NotNull().NotEmpty().WithMessage("PutAccess is required.");
            RuleFor(x => x.isDeleteAccess).NotNull().NotEmpty().WithMessage("DeleteAccess is required.");
        }
    }
}

