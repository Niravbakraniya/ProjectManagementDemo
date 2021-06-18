using ProductManagement.Api.ViewModel;
using ProductManagement.Api.ViewModel.Category;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProductManagement.Api.Repository
{
    public interface ICategoryRepository
    {
        Task<int> AddUpdateCategory(AddUpdateCategoryViewModel model);
        Task<CategoryViewModel> GetCategoryDetailByID(int id);
        Task<bool> IsCategoryExists(int CategoryID, string CategoryName);
        Task<ListResponseViewModel<CategoryViewModel>> GetCategoryList(ListRequestViewModel model);
        Task<int> DeleteCategory(int[] ids);
        Task<int> ChangeStatus(int id, bool isActive);
        Task<List<DropdownViewModel>> GetCategoryNameList();
    }
}