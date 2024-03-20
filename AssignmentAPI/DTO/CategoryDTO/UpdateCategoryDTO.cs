using System;
using AssignmentAPI.Models;
using FluentValidation;

namespace AssignmentAPI.DTO.CategoryDTO
{
	public class UpdateCategoryDTO
	{
        public string? CategoryId { get; set; }

        public string? CategoryCode { get; set; }

        public string? CategoryName { get; set; }

        public string? ParentCategoryId { get; set; }

    }
    public class UpdateCategoryDTOValidator : AbstractValidator<UpdateCategoryDTO>
    {
        public UpdateCategoryDTOValidator()
        {
            RuleFor(x => x.CategoryId).NotNull().NotEmpty().MaximumLength(255).WithMessage("CategoryCode is required.");
            RuleFor(x => x.CategoryCode).NotNull().NotEmpty().MaximumLength(255).WithMessage("CategoryCode is required.");
            RuleFor(x => x.CategoryName).NotNull().NotEmpty().WithMessage("CategoryName is required.");
            RuleFor(x => x.ParentCategoryId).NotNull().NotEmpty().WithMessage("ParentCategory is required.");
        }
    }
}

