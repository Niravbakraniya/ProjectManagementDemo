using ProductManagement.Api.ViewModel;
using ProductManagement.Api.ViewModel.Product;
using System.Threading.Tasks;

namespace ProductManagement.Api.Repository
{
    public interface IProductRepository
    {
        Task<int> AddUpdateProduct(AddUpdateProductViewModel model);
        Task<ProductViewModel> GetProductDetailByID(int id);
        Task<bool> IsProductExists(int ProductID, string ProductName);
        Task<ListResponseViewModel<ProductViewModel>> GetProductList(ListRequestViewModel model);
        Task<int> DeleteProduct(int[] ids);
        Task<int> ChangeStatus(int id, bool isActive);
    }
}
