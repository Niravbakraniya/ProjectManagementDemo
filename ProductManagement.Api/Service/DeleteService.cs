using ProductManagement.Api.Common.Result;
using ProductManagement.Api.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using static ProductManagement.Api.Common.GlobalCode;

namespace ProductManagement.Api.Service
{
    public class DeleteService : IDeleteService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IProductRepository _productRepository;

        public DeleteService(ICategoryRepository categoryRepository,IProductRepository productRepository)
        {
            _categoryRepository = categoryRepository;
            _productRepository = productRepository;
        }

        public async Task<ServiceResultExt<bool>> Delete(string moduleName, int[] id)
        {
            int result = 0;
            if (moduleName == ModuleName.Category.ToString())
            {
                result = await _categoryRepository.DeleteCategory(id);
            }
            else if (moduleName == ModuleName.Product.ToString())
            {
                result = await _productRepository.DeleteProduct(id);
            }
            if (result > 0)
            {
                return new ServiceResultExt<bool> { status = (int)HttpStatusCode.OK, ResultObject = true, message = "Records deleted successfully!" };
            }
            return new ServiceResultExt<bool> { status = (int)HttpStatusCode.BadRequest, message = "Request is invalid" };
        }
    }
}
