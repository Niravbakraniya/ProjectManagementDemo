using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagement.Api.ViewModel
{
    public class ListRequestViewModel
    {
        public string searchText { get; set; }
        public bool? isActive { get; set; }
        public int page { get; set; }
        public int size { get; set; }
    }
}
