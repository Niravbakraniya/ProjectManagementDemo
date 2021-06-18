using ProductManagement.Api.ViewModel.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagement.Api.ViewModel.Product
{
    public class ProductViewModel
    {
        public int ProductID { get; set; }

        public string ProductName { get; set; }

        public decimal Cost { get; set; }

        public string Description { get; set; }

        public bool IsActive { get; set; }

        public List<DropdownViewModel> CategoryIDs { get; set; } 
    }
}
