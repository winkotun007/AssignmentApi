using System;
using AssignmentAPI.DTO.RoomDTO;
using FluentValidation;

namespace AssignmentAPI.DTO.LevelDTO
{
	public class CreateLevelDTO
	{
        public string LevelName { get; set; }

        public string LevelCode { get; set; }

        public string BuildingId { get; set; }
    }

    public class LevelCreateDTOValidator : AbstractValidator<CreateLevelDTO>
    {
        public LevelCreateDTOValidator()
        {
            RuleFor(x => x.LevelName).NotEmpty().MaximumLength(255).WithMessage("Level Name is required.");
            RuleFor(x => x.LevelCode).NotEmpty().MaximumLength(50).WithMessage("Level Code is required.");
            RuleFor(x => x.BuildingId).NotEmpty().MaximumLength(50).WithMessage("Building is required.");
        }
    }
}

