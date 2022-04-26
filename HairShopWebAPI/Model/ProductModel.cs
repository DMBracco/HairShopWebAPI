namespace HairShopWebAPI.Model
{
    public class ProductModel
    {
        public int productID { get; set; }

        public string? productName { get; set; }

        public decimal unitPrice { get; set; }

        public int countStock { get; set; }

        public int? volume { get; set; }

        public int discountID { get; set; }
        public int discountAmount { get; set; }

        public int brandID { get; set; }
        public string? brandName { get; set; }

        public int productTypeID { get; set; }
        public string? productTypeName { get; set; }

        public int hairTypeID { get; set; }
        public string? hairTypeName { get; set; }

        public bool check { get; set; }
    }
}
