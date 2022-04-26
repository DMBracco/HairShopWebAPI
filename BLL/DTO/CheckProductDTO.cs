namespace BLL.DTO
{
    public class CheckProductDTO
    {
        public int CheckID { get; set; }
        public int ProductID { get; set; }
        public string? ProductName { get; set; }
        public int ProductQuantity { get; set; }
        public decimal ProductPrice { get; set; }
        public decimal ProductSumma { get; set; }
        public decimal ProductDiscount { get; set; }
        public decimal ProductSumItog { get; set; }
    }
}
