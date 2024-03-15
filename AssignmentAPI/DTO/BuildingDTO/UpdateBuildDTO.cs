using System;
using AssignmentAPI.DTO.BuildingDTO;
using FluentValidation;

namespace AssignmentAPI.DTO.BuildingDTO
{
    public class UpdateBuildDTO
    {
        public string? BuildingId { get; set; }
        public string? BuildingName { get; set; }
        public string? BuildingCode { get; set; }
    }

    public class BuildingUpdateDTOValidator : AbstractValidator<UpdateBuildDTO>
    {
        public BuildingUpdateDTOValidator()
        {
            RuleFor(x => x.BuildingId).NotEmpty().MaximumLength(255).WithMessage("Building ID  is required.");
            RuleFor(x => x.BuildingName).NotEmpty().MaximumLength(255).WithMessage("Building Name is required.");
            RuleFor(x => x.BuildingCode).NotEmpty().MaximumLength(50).WithMessage("Building Code is required.");
        }
    }
}


