using ProductManagement.Api.Common.Result;
using ProductManagement.Api.Repository;
using ProductManagement.Api.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using static ProductManagement.Api.Common.GlobalCode;

namespace ProductManagement.Api.Service
{
    public class StatusService : IStatusService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IProductRepository _productRepository;

        public StatusService(ICategoryRepository categoryRepository, IProductRepository productRepository)
        {
            _categoryRepository = categoryRepository;
            _productRepository = productRepository;
        }

        public async Task<ServiceResultExt<bool>> ChangeStatus(ChangeStatusModel model)
        {
            int result = 0;
            if (model.ModuleName == ModuleName.Category.ToString())
            {
                result = await _categoryRepository.ChangeStatus(model.Id, model.IsActive);
            }
            else if (model.ModuleName == ModuleName.Product.ToString())
            {
                result = await _productRepository.ChangeStatus(model.Id, model.IsActive);
            }
            if (result > 0)
            {
                return new ServiceResultExt<bool> { status = (int)HttpStatusCode.OK, ResultObject = true, message = "Status changed successfully!" };
            }
            return new ServiceResultExt<bool> { status = (int)HttpStatusCode.BadRequest, message = "Request is invalid" };
        }
    }
}
