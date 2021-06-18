using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductManagement.Api.Service;
using ProductManagement.Api.ViewModel;
using ProductManagement.Api.ViewModel.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductManagement.Controllers
{
    [ApiController]
    [Route("v1/api/category")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        /// <summary>
        /// Create Category
        /// </summary>
        /// <returns>Service Result</returns>
        [HttpPost()]
        public async Task<IActionResult> CreateCategory([FromBody] AddUpdateCategoryViewModel model)
        {
            return Ok(await _categoryService.Create(model));
        }

        /// <summary>
        /// Update Category
        /// </summary>
        /// <returns>Service Result</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatCategory(int id, [FromBody] AddUpdateCategoryViewModel model)
        {
            model.CategoryID = id; ;
            return Ok(await _categoryService.Update(model));
        }

        /// <summary>
        /// GetCategoryList
        /// </summary>
        /// <returns>Get Category List Model</returns>
        [HttpPost("get-category-list")]
        public async Task<IActionResult> GetCategoryList([FromBody] ListRequestViewModel reqmodel) => Ok(await _categoryService.GetCategoryList(reqmodel));

        /// <summary>
        /// GetCategoryDetail
        /// </summary>
        /// <returns>Category List Model</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategoryDetail(int id) => Ok(await _categoryService.GetDetail(id));

        /// <summary>
        /// GetCategoryNameDropdown
        /// </summary>
        /// <returns>CategoryNameDropdown</returns>
        [HttpGet("get-categoryName-dropdown")]
        public async Task<IActionResult> GetCategoryNameList() => Ok(await this._categoryService.GetCategoryNameList());

    }
}
