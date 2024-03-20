using System;
namespace AssignmentAPI.Models
{
	public class CategoryModel : BaseModel
	{
		public string? CategoryId { get; set; }

		public string? CategoryCode { get; set; }

		public string? CategoryName { get; set; }

		public string? ParentCategoryId { get; set; }

		public virtual CategoryModel ParentCategory { get; set; }

		public virtual ICollection<CategoryModel> ChildCategories { get; set; }
	}
}

