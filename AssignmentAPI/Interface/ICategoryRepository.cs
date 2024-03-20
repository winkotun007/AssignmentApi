using AssignmentAPI.DTO.CategoryDTO;
using AssignmentAPI.Models;
using AssignmentAPI.Shared;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AssignmentAPI.Interface
{
    public interface ICategoryRepository
    {
        Task<ResponseModel<List<CategoryModel>>> GetTreeViewCategorys();
        Task<ResponseModel<IEnumerable<CategoryModel>>> GetCategorysAsync();
        Task<CategoryModel> GetCategoryByIdAsync(string id);
        Task<ResponseModel<CategoryModel>> CreateCategoryAsync(CreateCategoryDTO CategoryDTO);
        Task<ResponseModel<CategoryModel>> UpdateCategoryAsync(string id, UpdateCategoryDTO updateCategoryDTO);
        Task<IActionResult> DeleteCategoryAsync(string id);
        Task<ResponseModel<List<CategoryModel>>> GetFlattenCategorys();
    }
}
