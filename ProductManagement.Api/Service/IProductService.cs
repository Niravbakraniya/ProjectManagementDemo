using ProductManagement.Api.Common.Result;
using ProductManagement.Api.ViewModel;
using ProductManagement.Api.ViewModel.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagement.Api.Service
{
    public interface IProductService
    {
        Task<ServiceResultExt<ListResponseViewModel<ProductViewModel>>> GetProductList(ListRequestViewModel model);
        Task<ServiceResultExt<int>> Create(AddUpdateProductViewModel request);
        Task<ServiceResultExt<bool>> Update(AddUpdateProductViewModel request);
        Task<ServiceResultExt<ProductViewModel>> GetDetail(int id);
    }
}
