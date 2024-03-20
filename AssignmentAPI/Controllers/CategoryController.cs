using Microsoft.AspNetCore.Mvc;
using AssignmentAPI.Models;
using AssignmentAPI.Shared;
using AssignmentAPI.DTO.CategoryDTO;
using AssignmentAPI.Interface;
using Microsoft.AspNetCore.Authorization;
using AutoMapper;
using AssignmentAPI.Repository;

namespace AssignmentAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository _CategoryRepository;
        private readonly ILogger<CategoryController> _logger;
        private readonly IMapper _mapper;
        public CategoryController(ICategoryRepository CategoryRepository, ILogger<CategoryController> logger,IMapper mapper)
        {
            _mapper=mapper;
            _CategoryRepository = CategoryRepository;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<ResponseModel<IEnumerable<CategoryModel>>>> GetCategory()
        {
            _logger.LogInformation("Get Category");

            return await _CategoryRepository.GetCategorysAsync();
        }

        [HttpGet("GetTreeView")]
        public async Task<ActionResult<ResponseModel<List<CategoryModel>>>> GetTreeView()
        {
            _logger.LogInformation("Get TreeView Category");

            return await _CategoryRepository.GetTreeViewCategorys();
        }

        [HttpGet("GetTreeNode")]
        public async Task<ActionResult<ResponseModel<List<TreeNode>>>> GetTreeNode()
        {
            _logger.LogInformation("Get Tree Node Category");

            ResponseModel<List<TreeNode>> response = new ResponseModel<List<TreeNode>>();

            try {
                var treeCategory = await _CategoryRepository.GetTreeViewCategorys();
                List<CategoryModel> categoryModels = treeCategory.Data.ToList();
                //dd

                List<TreeNode> treeNodes = _mapper.Map<List<TreeNode>>(categoryModels);

                response.Code = 200;
                response.Message = ResponseMessage.GetSuccessfull;
                response.Data = treeNodes;

            }
            catch(Exception)
            {
                response.Code = 500;
                response.Data = null;
                response.Message = ResponseMessage.InternalServerError;
            }

            return response;    
        }

        [HttpGet("GetFlattenList")]
        public async Task<ActionResult<ResponseModel<List<CategoryModel>>>> GetFlattenList()
        {
            _logger.LogInformation("Get Flatten  Category List");

            return await _CategoryRepository.GetFlattenCategorys();
        }


        [HttpGet("GetCategoryByID")]
        public async Task<ActionResult<CategoryModel>> GetCategoryByID(IDModel iDModel)
        {
            _logger.LogInformation("Get Category By ID");
            return await _CategoryRepository.GetCategoryByIdAsync(iDModel.Id);
        }

        [HttpPost]
        public async Task<ActionResult<ResponseModel<CategoryModel>>> PostCategory(CreateCategoryDTO CategoryDTO)
        {
            _logger.LogInformation("Cretate New Category.");
            return await _CategoryRepository.CreateCategoryAsync(CategoryDTO);
        }

        [HttpPut]
        public async Task<ActionResult<ResponseModel<CategoryModel>>> PutCategory(UpdateCategoryDTO updateCategoryDTO)
        {
            _logger.LogInformation("Update Existing Buidling.");
            return await _CategoryRepository.UpdateCategoryAsync(updateCategoryDTO.CategoryId, updateCategoryDTO);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteCategory(IDModel iDModel)
        {
            _logger.LogInformation("Delete By Buidling ID.");
            return await _CategoryRepository.DeleteCategoryAsync(iDModel.Id);
        }
    }
}
