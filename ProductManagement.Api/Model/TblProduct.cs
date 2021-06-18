using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductManagement.Api.Model
{
    public class TblProduct
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int ProductID { get; set; }

        [Column(TypeName = "VARCHAR(50)")]
        public string ProductName { get; set; }

        [Column(TypeName = "DECIMAL(24,2)")]
        public decimal Cost { get; set; }

        [Column(TypeName = "VARCHAR(550)")]
        public string Description { get; set; }

        public bool IsActive { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime ModifiedDate { get; set; }
    }
}
