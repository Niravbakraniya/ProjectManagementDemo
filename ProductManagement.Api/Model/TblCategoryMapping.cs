using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductManagement.Api.Model
{
    public class TblCategoryMapping
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int CategoryMappingID { get; set; }

        [ForeignKey("TblCategory")]
        public int CategoryID { get; set; }
        public virtual TblCategory TblCategory { get; set; }

        [ForeignKey("TblProduct")]
        public int ProductID { get; set; }
        public virtual TblProduct TblProduct { get; set; } 
    }
}