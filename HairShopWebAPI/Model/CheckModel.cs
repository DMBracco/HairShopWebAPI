namespace HairShopWebAPI.Model
{
    public class CheckModel
    {
        public int checkID { get; set; }

        public DateTime date { get; set; }

        public decimal totalPrice { get; set; }

        public int userID { get; set; }

        public List<ProductModel>? productsModel { get; set; }
    }
}
