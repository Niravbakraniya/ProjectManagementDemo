using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductManagement.Api.Service;
using ProductManagement.Api.ViewModel;
using ProductManagement.Api.ViewModel.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductManagement.Controllers
{
    [ApiController]
    [Route("v1/api/product")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        /// <summary>
        /// Create Product
        /// </summary>
        /// <returns>Service Result</returns>
        [HttpPost()]
        public async Task<IActionResult> CreateProduct([FromBody] AddUpdateProductViewModel model)
        {
            return Ok(await _productService.Create(model));
        }

        /// <summary>
        /// Update Product
        /// </summary>
        /// <returns>Service Result</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatProduct(int id, [FromBody] AddUpdateProductViewModel model)
        {
            model.ProductID = id; ;
            return Ok(await _productService.Update(model));
        }

        /// <summary>
        /// GetProductList
        /// </summary>
        /// <returns>Get Product List Model</returns>
        [HttpPost("get-product-list")]
        public async Task<IActionResult> GetProductList([FromBody] ListRequestViewModel reqmodel) => Ok(await _productService.GetProductList(reqmodel));

        /// <summary>
        /// GetProductDetail
        /// </summary>
        /// <returns>Product List Model</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductDetail(int id) => Ok(await _productService.GetDetail(id));
    }
}
