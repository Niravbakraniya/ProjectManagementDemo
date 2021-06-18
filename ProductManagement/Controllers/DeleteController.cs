using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductManagement.Api.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductManagement.Controllers
{
    [Route("v1/api/delete")]
    public class DeleteController : ControllerBase
    {
        private readonly IDeleteService _deleteService;

        public DeleteController(IDeleteService deleteService)
        {
            _deleteService = deleteService;
        }

        /// <summary>
        /// delete
        /// </summary>
        /// <returns>Service Result</returns>
        [HttpDelete()]
        public async Task<IActionResult> Delete(string ModuleName, int[] id) => Ok(await _deleteService.Delete(ModuleName, id));
    }
}
