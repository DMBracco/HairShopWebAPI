namespace HairShopWebAPI.Model
{
    public class SupplyModel
    {
        public int supplyID { get; set; }

        public string? invoice { get; set; }

        public DateTime data { get; set; }

        public int supplierID { get; set; }

        public string? supplierName { get; set; }

        public int userID { get; set; }

        public List<ProductModel> productsModel { get; set; }
    }
}
