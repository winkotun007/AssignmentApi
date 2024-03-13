using System;
using FluentValidation;

namespace AssignmentAPI.DTO.RoomDTO
{
    public class CreateRoomDTO
    {
        public string RoomName { get; set; }

        public string RoomCode { get; set; }

        public string LevelId { get; set; }
    }

    public class BuildingCreateDTOValidator : AbstractValidator<CreateRoomDTO>
    {
        public BuildingCreateDTOValidator()
        {
            RuleFor(x => x.RoomName).NotEmpty().MaximumLength(255).WithMessage("Room Name is required."); 
            RuleFor(x => x.RoomCode).NotEmpty().MaximumLength(50).WithMessage("Room Code is required."); 
            RuleFor(x => x.LevelId).NotEmpty().MaximumLength(50).WithMessage("Level is required."); 
        }
    }
}

