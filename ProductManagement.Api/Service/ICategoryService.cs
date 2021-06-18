using ProductManagement.Api.Common.Result;
using ProductManagement.Api.ViewModel;
using ProductManagement.Api.ViewModel.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagement.Api.Service
{
    public interface ICategoryService
    {
        Task<ServiceResultExt<ListResponseViewModel<CategoryViewModel>>> GetCategoryList(ListRequestViewModel model);
        Task<ServiceResultExt<int>> Create(AddUpdateCategoryViewModel request);
        Task<ServiceResultExt<bool>> Update(AddUpdateCategoryViewModel request);
        Task<ServiceResultExt<CategoryViewModel>> GetDetail(int id);
        Task<ServiceResultExt<List<DropdownViewModel>>> GetCategoryNameList();
    }
}
