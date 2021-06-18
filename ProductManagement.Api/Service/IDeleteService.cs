using ProductManagement.Api.Common.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagement.Api.Service
{
    public interface IDeleteService
    {
        Task<ServiceResultExt<bool>> Delete(string ModuleName, int[] id);
    }
}
