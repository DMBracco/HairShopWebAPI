using System.ComponentModel.DataAnnotations;

namespace BLL.Entities
{
    public class Supply
    {
        [Key]
        public int SupplyID { get; set; }

        [Required]
        [StringLength(50)]
        public string? Invoice { get; set; }

        public DateTime Data { get; set; }

        public int SupplierID { get; set; }
        public Supplier? Supplier { get; set; }

        public User? User { get; set; }

        public List<SupplyForProduct> SupplyForProducts { get; set; } = new();
    }
}
