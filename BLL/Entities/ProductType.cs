using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BLL.Entities
{
    public class ProductType
    {
        [Key]
        public int ProductTypeID { get; set; }

        [Required]
        [StringLength(50)]
        public string? ProductTypeName { get; set; }

        public virtual List<Product> Products { get; set; } = new();
    }
}
