using System;
using AssignmentAPI.DTO.GuestAccessDTO;
using AssignmentAPI.Models;
using FluentValidation;

namespace AssignmentAPI.DTO.CategoryDTO
{
	public class CreateCategoryDTO
	{

        public string? CategoryCode { get; set; }

        public string? CategoryName { get; set; }

        public string? ParentCategoryId { get; set; }

    }
    public class CreateCategoryDTOValidator : AbstractValidator<CreateCategoryDTO>
    {
        public CreateCategoryDTOValidator()
        {
            RuleFor(x => x.CategoryCode).NotNull().NotEmpty().MaximumLength(255).WithMessage("CategoryCode is required.");
            RuleFor(x => x.CategoryName).NotNull().NotEmpty().WithMessage("CategoryName is required.");
        }
    }
}

