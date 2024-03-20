using System;
namespace AssignmentAPI.DTO.CategoryDTO
{
	public class TreeNode
	{
        public string? CategoryCode { get; set; }

        public string? CategoryName { get; set; }

        public string? ParentCategoryId { get; set; }

        public virtual List<TreeNode>? children { get; set; }
    }
}

