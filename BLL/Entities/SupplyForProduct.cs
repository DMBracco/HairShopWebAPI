namespace BLL.Entities
{
    public class SupplyForProduct
    {
        public int SupplyID { get; set; }
        public Supply? Supply { get; set; }

        public int ProductID { get; set; }
        public Product? Product { get; set; }

        public decimal PurchasingPrice { get; set; }

        public int ProductQuantity { get; set; }
    }
}
