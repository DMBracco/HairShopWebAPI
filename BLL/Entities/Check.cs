namespace BLL.Entities
{
    public class Check
    {
        public int CheckID { get; set; }

        public DateTime Date { get; set; }

        public decimal TotalPrice { get; set; }

        public int UserID { get; set; }
        public User? User { get; set; }

        public List<CheckForProduct> CheckForProducts { get; set; } = new();
    }
}
