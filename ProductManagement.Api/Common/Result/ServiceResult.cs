using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagement.Api.Common.Result
{
    public class ServiceResult
    {
        public ServiceResult()
        {
            this.status = (int)HttpStatusCode.OK;
        }
        public int status { get; set; }
        public string message { get; set; }
    }

    public class ServiceResultExt<T> : ServiceResult
    {
        public ServiceResultExt()
        {
            this.status = (int)HttpStatusCode.OK;
            this.ResultObject = default;
        }
        public T ResultObject { get; set; }
    }
}
