using System;
using FluentValidation;

namespace AssignmentAPI.DTO.RoomDTO
{
    public class UpdateRoomDTO
    {
        public string RoomId { get; set; }

        public string RoomName { get; set; }

        public string RoomCode { get; set; }

        public string LevelId { get; set; }
    }

    public class BuildingUpdateDTOValidator : AbstractValidator<UpdateRoomDTO>
    {
        public BuildingUpdateDTOValidator()
        {
            RuleFor(x => x.RoomId).NotEmpty().MaximumLength(255).WithMessage("Room Name is required.");
            RuleFor(x => x.RoomName).NotEmpty().MaximumLength(255).WithMessage("Room Name is required."); 
            RuleFor(x => x.RoomCode).NotEmpty().MaximumLength(50).WithMessage("Room Code is required."); 
            RuleFor(x => x.LevelId).NotEmpty().MaximumLength(50).WithMessage("Level is required."); 
        }
    }
}

