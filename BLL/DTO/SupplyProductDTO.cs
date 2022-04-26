namespace BLL.DTO
{
    public class SupplyProductDTO
    {
        public int SupplyID { get; set; }
        public int ProductID { get; set; }
        public string? ProductName { get; set; }
        public int ProductQuantity { get; set; }
        public decimal ProductPrice { get; set; }
    }
}
