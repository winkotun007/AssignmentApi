using System;
using System.Net;
using AssignmentAPI.DTO.CategoryDTO;
using AssignmentAPI.Interface;
using AssignmentAPI.Models;
using AssignmentAPI.Shared;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AssignmentAPI.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly AssignmentDBContext _context;
        private readonly IMapper _mapper;

        public CategoryRepository(AssignmentDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public List<CategoryModel> FlattenCategoryList(List<CategoryModel> categoryList, string parentCategoryId)
        {
            var flatList = new List<CategoryModel>();

            var children = categoryList.Where(ex => ex.ParentCategoryId == parentCategoryId).ToList();
            foreach (var child in children)
            {
                var node = new CategoryModel
                {
                    CategoryId = child.CategoryId,
                    CategoryCode = child.CategoryCode,
                    CategoryName = child.CategoryName,
                    ParentCategoryId = child.ParentCategoryId
                };

                flatList.Add(node);
                var grandchildren = FlattenCategoryList(categoryList, child.CategoryId);
                flatList.AddRange(grandchildren);
            }

            return flatList;
        }

        private List<CategoryModel> BuildTreeView(List<CategoryModel> categoryList, string ParentCategoryID)
        {
            var tree = new List<CategoryModel>();

            var children = categoryList.Where(ex => ex.ParentCategoryId == ParentCategoryID).ToList();

            foreach (var child in children)
            {
                var node = new CategoryModel
                {
                    CategoryId = child.CategoryId,
                    CategoryCode = child.CategoryCode,
                    CategoryName = child.CategoryName,
                    ParentCategoryId = child.ParentCategoryId,
                    ChildCategories = BuildTreeView(categoryList, child.CategoryId)
                };

                tree.Add(node);
            }

            return tree;
        }


        public async Task<ResponseModel<List<CategoryModel>>> GetFlattenCategorys()
        {
            ResponseModel<List<CategoryModel>> response = new ResponseModel<List<CategoryModel>>();
            try
            {
                List<CategoryModel> categoryList = await _context.Category.ToListAsync();
                if (categoryList != null)
                {
                    var getCategoryList = _mapper.Map<List<CategoryModel>>(categoryList);

                    response.Code = (int)HttpStatusCode.OK;
                    response.Data = FlattenCategoryList(getCategoryList, null);
                    response.Message = ResponseMessage.GetSuccessfull;
                }
            }
            catch (Exception)
            {
                response.Code = (int)HttpStatusCode.InternalServerError;
                response.Message = ResponseMessage.InternalServerError;
            }
            return response;
        }


        public async Task<ResponseModel<List<CategoryModel>>> GetTreeViewCategorys()
        {
            ResponseModel<List<CategoryModel>> response = new ResponseModel<List<CategoryModel>>();
            try
            {
                List<CategoryModel> categoryList = await _context.Category.ToListAsync();
                if (categoryList != null)
                {
                    var getCategoryList = _mapper.Map<List<CategoryModel>>(categoryList);

                    response.Code = (int)HttpStatusCode.OK;
                    response.Data = BuildTreeView(getCategoryList, null);
                    response.Message = ResponseMessage.GetSuccessfull;
                }
            }
            catch (Exception)
            {
                response.Code = (int)HttpStatusCode.InternalServerError;
                response.Message = ResponseMessage.InternalServerError;
            }
            return response;
        }


        public async Task<ResponseModel<IEnumerable<CategoryModel>>> GetCategorysAsync()
        {
            ResponseModel<IEnumerable<CategoryModel>> responseModel = new ResponseModel<IEnumerable<CategoryModel>>();

            try
            {
                List<CategoryModel> getCategorys = await _context.Category.ToListAsync();

                if (getCategorys.Count == 0)
                {
                    responseModel.Code = (int)HttpStatusCode.NoContent;
                    responseModel.Data = null;
                }
                else
                {
                    responseModel.Code = (int)HttpStatusCode.OK;
                    responseModel.Data = getCategorys;
                }
            }
            catch (Exception)
            {
                responseModel.Code = (int)HttpStatusCode.InternalServerError;
            }

            return responseModel;
        }

       
        public async Task<CategoryModel> GetCategoryByIdAsync(string id)
        {
            return await _context.Category.Where(ex => ex.CategoryId == id).FirstOrDefaultAsync();
        }

        public async Task<ResponseModel<CategoryModel>> CreateCategoryAsync(CreateCategoryDTO CategoryDTO)
        {
            ResponseModel<CategoryModel> responseModel = new ResponseModel<CategoryModel>();

            try
            {
                CategoryModel Category = _mapper.Map<CategoryModel>(CategoryDTO);

                _context.Category.Add(Category);

                await _context.SaveChangesAsync();

                responseModel.Code = (int)HttpStatusCode.Created;
                responseModel.Message = ResponseMessage.SaveSuccessful;
                responseModel.Data = Category;
            }
            catch (Exception)
            {
                responseModel.Code = (int)HttpStatusCode.InternalServerError;
                responseModel.Message = ResponseMessage.InternalServerError;
            }

            return responseModel;
        }

        public async Task<ResponseModel<CategoryModel>> UpdateCategoryAsync(string id, UpdateCategoryDTO updateCategoryDTO)
        {
            ResponseModel<CategoryModel> responseModel = new ResponseModel<CategoryModel>();

            if (id != updateCategoryDTO.CategoryId)
            {
                responseModel.Code = (int)HttpStatusCode.BadRequest;
                responseModel.Message = ResponseMessage.InternalServerError;
                return responseModel;
            }

            try
            {
                CategoryModel existingCategory = await _context.Category.FindAsync(id);

                if (existingCategory == null)
                {
                    responseModel.Code = (int)HttpStatusCode.NotFound;
                    responseModel.Message = ResponseMessage.NotFound;
                    return responseModel;
                }

                _mapper.Map(updateCategoryDTO, existingCategory);

                _context.Entry(existingCategory).State = EntityState.Modified;

                await _context.SaveChangesAsync();

                responseModel.Code = (int)HttpStatusCode.OK;
                responseModel.Message = ResponseMessage.UpdateSuccessful;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoryExists(id))
                {
                    responseModel.Code = (int)HttpStatusCode.NotFound;
                    responseModel.Message = ResponseMessage.NotFound;
                }
                else
                {
                    responseModel.Code = (int)HttpStatusCode.InternalServerError;
                    responseModel.Message = ResponseMessage.InternalServerError;
                }
            }

            return responseModel;
        }

        public async Task<IActionResult> DeleteCategoryAsync(string id)
        {
            var Category = await _context.Category.FindAsync(id);
            if (Category == null)
            {
                return new NotFoundResult();
            }

            _context.Category.Remove(Category);
            await _context.SaveChangesAsync();

            return new NoContentResult();
        }

        private bool CategoryExists(string id)
        {
            return _context.Category.Any(e => e.CategoryId == id);
        }

    }
}



//using System;
//using AssignmentAPI.Shared;

//namespace AssignmentAPI.Repository
//{
//	public class CategoryRepository
//	{
//private List<GetListCategory> FlattenCategoryList(List<GetListCategory> deptList, string parentCategoryId, string parentPathDescription)
//{
//    var flatList = new List<GetListCategory>();

//    var children = deptList.Where(ex => ex.ParentCategoryId == parentCategoryId).ToList();
//    foreach (var child in children)
//    {
//        var node = new GetListCategory
//        {
//            CategoryId = child.CategoryId,
//            Code = child.Code,
//            Name = child.Name,
//            Description = child.Description,
//            ParentCategoryId = child.ParentCategoryId,
//            ParentPathDescription = ((child.ParentCategoryId != null) ? parentPathDescription + " >> " + child.Name : child.Name)
//        };

//        flatList.Add(node);
//        var grandchildren = FlattenCategoryList(deptList, child.CategoryId, node.ParentPathDescription);
//        flatList.AddRange(grandchildren);
//    }

//    return flatList;
//}

//private List<GetListCategory> BuildTreeView(List<GetListCategory> deptList, string ParentCategoryID, string ParentPathDescription)
//{
//    var tree = new List<GetListCategory>();

//    var children = deptList.Where(ex => ex.ParentCategoryId == ParentCategoryID).ToList();

//    foreach (var child in children)
//    {
//        var node = new GetListCategory
//        {
//            CategoryId = child.CategoryId,
//            Code = child.Code,
//            Name = child.Name,
//            Description = child.Description,
//            ParentCategoryId = child.ParentCategoryId,
//            ParentPathDescription = string.IsNullOrEmpty(ParentPathDescription) ? child.Name : ParentPathDescription + " >> " + child.Name,
//            children = BuildTreeView(deptList, child.CategoryId, (string.IsNullOrEmpty(ParentPathDescription) ? child.Name : ParentPathDescription + " >> " + child.Name))
//        };

//        tree.Add(node);
//    }

//    return tree;
//}

//public async Task<GetResponse<List<GetListCategory>>> GetTreeViewCategorys()
//{
//    GetResponse<List<GetListCategory>> response = new GetResponse<List<GetListCategory>>();
//    try
//    {
//        List<Category> DeptList = await _CategoryRepository.GetALL_Category();
//        if (DeptList != null)
//        {
//            var getDeptList = _mapper.Map<List<GetListCategory>>(DeptList);
//            response.IsSuccess = true;
//            response.Data = BuildTreeView(getDeptList, null, null);
//            response.Message = _localizer[ResponseMessages.GETALL_SUCCESSFUL].Value;
//        }
//    }
//    catch (Exception)
//    {
//        response.IsSuccess = false;
//        response.Message = _localizer[ResponseMessages.ERR_INTERNAL_SERVER].Value;
//    }
//    return response;
//}

//    }
//}

