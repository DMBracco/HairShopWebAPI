namespace HairShopWebAPI.Model
{
    public class DiscountModel
    {
        public int discountID { get; set; }

        public string? discountName { get; set; }

        public int discountAmount { get; set; }

        public DateTime dateStart { get; set; }

        public DateTime dateEnd { get; set; }

        public int productID { get; set; }
    }
}
