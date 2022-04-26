namespace BLL.Entities
{
    public class CheckForProduct
    {
        public int CheckID { get; set; }
        public Check? Check { get; set; }

        public int ProductID { get; set; }
        public Product? Product { get; set; }

        public int ProductQuantity { get; set; }
    }
}
