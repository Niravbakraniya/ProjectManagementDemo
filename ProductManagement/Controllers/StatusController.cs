using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductManagement.Api.Service;
using ProductManagement.Api.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductManagement.Controllers
{
    [Route("v1/api/status")]
    public class StatusController : ControllerBase
    {
        private readonly IStatusService _statusService;

        public StatusController(IStatusService statusService)
        {
            _statusService = statusService;
        }

        /// <summary>
        /// Change Status
        /// </summary>
        /// <returns>Service Result</returns>
        [HttpPost()]
        public async Task<IActionResult> ChangeStatus([FromBody] ChangeStatusModel model) => Ok(await _statusService.ChangeStatus(model));
    }
}
