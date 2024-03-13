using System;
using AssignmentAPI.DTO.BuildingDTO;
using FluentValidation;

namespace AssignmentAPI.DTO.BuildingDTO
{
	public class CreateBuildingDTO
	{
        public string BuildingName { get; set; }
        public string BuildingCode { get; set; }
    }

    public class BuildingCreateDTOValidator : AbstractValidator<CreateBuildingDTO>
    {
        public BuildingCreateDTOValidator()
        {
            RuleFor(x => x.BuildingName).NotEmpty().MaximumLength(255).WithMessage("Building Name is required."); 
            RuleFor(x => x.BuildingCode).NotEmpty().MaximumLength(50).WithMessage("Building Code is required."); 
        }
    }
}

