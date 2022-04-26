using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BLL.Entities
{
    public class Supplier
    {
        [Key]
        public int SupplierID { get; set; }

        [Required]
        [StringLength(50)]
        public string? SupplierName { get; set; }

        [StringLength(50)]
        public string? Phonenumber { get; set; }

        public List<Supply> Supply { get; set; } = new();
    }
}
