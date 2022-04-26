using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BLL.Entities
{
    public class Discount
    {
        [Key]
        public int DiscountID { get; set; }

        [StringLength(50)]
        public string? DiscountName { get; set; }

        public int DiscountAmount { get; set; }

        public DateTime DateStart { get; set; }

        public DateTime DateEnd { get; set; }

        public int ProductID { get; set; }
        public Product? Product { get; set; }
    }
}
