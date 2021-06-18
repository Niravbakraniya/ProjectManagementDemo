using ProductManagement.Api.Common.Result;
using ProductManagement.Api.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagement.Api.Service
{
    public interface IStatusService
    {
        Task<ServiceResultExt<bool>> ChangeStatus(ChangeStatusModel model);
    }
}
