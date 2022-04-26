using System.ComponentModel.DataAnnotations;

namespace BLL.Entities
{
    public class Product
    {
        [Key]
        public int ProductID { get; set; }

        [Required]
        [StringLength(50)]
        public string? ProductName { get; set; }

        public decimal UnitPrice { get; set; }

        public int CountStock { get; set; }

        public int? Volume { get; set; }

        //public Discount Discount { get; set; } = new();

        public int BrandID { get; set; }
        public Brand? Brand { get; set; }

        public int ProductTypeID { get; set; }

        public int HairTypeID { get; set; }
        public HairType? HairType { get; set; }

        public List<SupplyForProduct> SupplyForProducts { get; set; } = new();

        public ProductType? ProductType { get; set; }

        public List<CheckForProduct> CheckForProducts { get; set; } = new();
    }
}
