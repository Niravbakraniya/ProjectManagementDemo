using System.Collections.Generic;

namespace ProductManagement.Api.ViewModel.Product
{
    public class AddUpdateProductViewModel
    {
        public int ProductID { get; set; }

        public string ProductName { get; set; }

        public decimal Cost { get; set; }

        public string Description { get; set; }

        public List<int> CategoryIDs { get; set; }
    }
}