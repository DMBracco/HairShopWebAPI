namespace HairShopWebAPI.Model
{
    public class UnionModel
    {
        public IEnumerable<BrandModel> Brands { get; set; } = new List<BrandModel>();
        public IEnumerable<ProductTypeModel> ProductTypes { get; set; } = new List<ProductTypeModel>();
        public IEnumerable<HairTypeModel> HairTypes { get; set; } = new List<HairTypeModel>();
    }
}
