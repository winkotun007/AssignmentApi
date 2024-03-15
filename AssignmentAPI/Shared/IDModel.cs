using System;
using AssignmentAPI.DTO.BuildingDTO;
using FluentValidation;

namespace AssignmentAPI.Shared
{
	public class IDModel
	{
        public string? Id { get; set; }
    }
    public class BuildingUpdateDTOValidator : AbstractValidator<IDModel>
    {
        public BuildingUpdateDTOValidator()
        {
            RuleFor(x => x.Id).NotEmpty().MaximumLength(255).WithMessage("ID  is required.");
        }
    }
}

