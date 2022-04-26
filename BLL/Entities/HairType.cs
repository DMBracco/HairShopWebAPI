using System.ComponentModel.DataAnnotations;

namespace BLL.Entities
{
    public class HairType
    {
        [Key]
        public int HairTypeID { get; set; }

        [Required]
        [StringLength(50)]
        public string? HairTypeName { get; set; }

        public List<Product> Product { get; set; } = new();
    }
}
