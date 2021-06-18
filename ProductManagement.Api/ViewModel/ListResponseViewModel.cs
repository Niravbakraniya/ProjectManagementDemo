using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagement.Api.ViewModel
{
    public class ListResponseViewModel<T> where T : class
    {
        public int page { get; set; }
        public int size { get; set; }
        public int totalcount { get; set; }
        public List<T> data { get; set; }
    }
}
