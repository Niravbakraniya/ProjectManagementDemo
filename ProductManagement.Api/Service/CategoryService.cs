using ProductManagement.Api.Common.Result;
using ProductManagement.Api.Repository;
using ProductManagement.Api.ViewModel;
using ProductManagement.Api.ViewModel.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagement.Api.Service
{
    public class CategoryService : ICategoryService
    {
        public readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<ServiceResultExt<ListResponseViewModel<CategoryViewModel>>> GetCategoryList(ListRequestViewModel model)
        {
            var res = await _categoryRepository.GetCategoryList(model);
            return new ServiceResultExt<ListResponseViewModel<CategoryViewModel>> { status = (int)HttpStatusCode.OK, ResultObject = res };
        }

        public async Task<ServiceResultExt<int>> Create(AddUpdateCategoryViewModel request)
        {
            var isExist = await _categoryRepository.IsCategoryExists(request.CategoryID, request.CategoryName);
            if (!isExist)
            {
                var res = await _categoryRepository.AddUpdateCategory(request);
                return new ServiceResultExt<int> { status = (int)HttpStatusCode.OK, ResultObject = res, message = "Category Detail Added successfully!" };
            }
            return new ServiceResultExt<int> { status = (int)HttpStatusCode.AlreadyReported, message = "Category Detail already exist." };

        }

        public async Task<ServiceResultExt<bool>> Update(AddUpdateCategoryViewModel request)
        {
            var isExist = await _categoryRepository.IsCategoryExists(request.CategoryID, request.CategoryName);
            if (isExist)
            {
                var res = await _categoryRepository.AddUpdateCategory(request);
                if (res == -1)
                    return new ServiceResultExt<bool> { status = (int)HttpStatusCode.NotFound, message = "Category Detail does not exist!" };
                return new ServiceResultExt<bool> { status = (int)HttpStatusCode.OK, ResultObject = res > 0, message = "Category Detail saved successfully!" };
            }
            return new ServiceResultExt<bool> { status = (int)HttpStatusCode.AlreadyReported, message = "Category Detail already exist." };
        }

        public async Task<ServiceResultExt<CategoryViewModel>> GetDetail(int id)
        {
            CategoryViewModel Category = await _categoryRepository.GetCategoryDetailByID(id);
            if (Category == null)
                return new ServiceResultExt<CategoryViewModel> { status = (int)HttpStatusCode.NotFound, message = "Category Detail does not exist!" };
            return new ServiceResultExt<CategoryViewModel> { status = (int)HttpStatusCode.OK, ResultObject = Category };
        }

        public async Task<ServiceResultExt<List<DropdownViewModel>>> GetCategoryNameList()
        {
            List<DropdownViewModel> category = await _categoryRepository.GetCategoryNameList();
            if (category == null)
                return new ServiceResultExt<List<DropdownViewModel>> { status = (int)HttpStatusCode.NotFound, message = "Category Name does not exist!" };
            return new ServiceResultExt<List<DropdownViewModel>> { status = (int)HttpStatusCode.OK, ResultObject = category };
        }
    }
}
