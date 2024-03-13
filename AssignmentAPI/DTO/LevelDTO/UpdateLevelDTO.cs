using System;
using AssignmentAPI.DTO.RoomDTO;
using FluentValidation;

namespace AssignmentAPI.DTO.LevelDTO
{
    public class UpdateLevelDTO
    {
        public string LevelId { get; set; }

        public string LevelName { get; set; }

        public string LevelCode { get; set; }

        public string BuildingId { get; set; }
    }

    public class LevelUpdateDTOValidator : AbstractValidator<UpdateLevelDTO>
    {
        public LevelUpdateDTOValidator()
        {
            RuleFor(x => x.LevelId).NotEmpty().MaximumLength(255).WithMessage("Level ID is required.");
            RuleFor(x => x.LevelName).NotEmpty().MaximumLength(255).WithMessage("Level Name is required."); 
            RuleFor(x => x.LevelCode).NotEmpty().MaximumLength(50).WithMessage("Level Code is required."); 
            RuleFor(x => x.BuildingId).NotEmpty().MaximumLength(50).WithMessage("Building is required."); 
        }
    }
}

