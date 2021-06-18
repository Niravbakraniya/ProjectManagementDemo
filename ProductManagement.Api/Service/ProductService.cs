using ProductManagement.Api.Common.Result;
using ProductManagement.Api.Repository;
using ProductManagement.Api.ViewModel;
using ProductManagement.Api.ViewModel.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagement.Api.Service
{
    public class ProductService : IProductService
    {
        public readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<ServiceResultExt<ListResponseViewModel<ProductViewModel>>> GetProductList(ListRequestViewModel model)
        {
            var res = await _productRepository.GetProductList(model);
            return new ServiceResultExt<ListResponseViewModel<ProductViewModel>> { status = (int)HttpStatusCode.OK, ResultObject = res };
        }

        public async Task<ServiceResultExt<int>> Create(AddUpdateProductViewModel request)
        {
            var isExist = await _productRepository.IsProductExists(request.ProductID, request.ProductName);
            if (!isExist)
            {
                var res = await _productRepository.AddUpdateProduct(request);
                return new ServiceResultExt<int> { status = (int)HttpStatusCode.OK, ResultObject = res, message = "Product Detail Added successfully!" };
            }
            return new ServiceResultExt<int> { status = (int)HttpStatusCode.AlreadyReported, message = "Product Detail already exist." };

        }

        public async Task<ServiceResultExt<bool>> Update(AddUpdateProductViewModel request)
        {
            var isExist = await _productRepository.IsProductExists(request.ProductID, request.ProductName);
            if (isExist)
            {
                var res = await _productRepository.AddUpdateProduct(request);
                if (res == -1)
                    return new ServiceResultExt<bool> { status = (int)HttpStatusCode.NotFound, message = "Product Detail does not exist!" };
                return new ServiceResultExt<bool> { status = (int)HttpStatusCode.OK, ResultObject = res > 0, message = "Product Detail saved successfully!" };
            }
            return new ServiceResultExt<bool> { status = (int)HttpStatusCode.AlreadyReported, message = "Product Detail already exist." };
        }

        public async Task<ServiceResultExt<ProductViewModel>> GetDetail(int id)
        {
            ProductViewModel Product = await _productRepository.GetProductDetailByID(id);
            if (Product == null)
                return new ServiceResultExt<ProductViewModel> { status = (int)HttpStatusCode.NotFound, message = "Product Detail does not exist!" };
            return new ServiceResultExt<ProductViewModel> { status = (int)HttpStatusCode.OK, ResultObject = Product };
        }
    }
}
