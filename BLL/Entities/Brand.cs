using System.ComponentModel.DataAnnotations;

namespace BLL.Entities
{
    public class Brand
    {
        [Key]
        public int BrandID { get; set; }

        [Required]
        [StringLength(50)]
        public string? BrandName { get; set; }

        public List<Product> Product { get; set; } = new();
    }
}
