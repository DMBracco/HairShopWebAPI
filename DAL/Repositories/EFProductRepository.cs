using BLL.Entities;
using BLL.IRepositories;
using DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories
{
    public class EFProductRepository : IRepository<Product>
    {
        private readonly ShopContext _shopContext;

        public EFProductRepository(ShopContext shopContext)
        {
            _shopContext = shopContext ?? throw new ArgumentNullException(nameof(shopContext));
        }

        public void Create(Product item)
        {
            _shopContext.Products.Add(item);
        }

        public bool Delete(int id)
        {
            var _shopContexItem = _shopContext.Products.Find(id);
            if (_shopContexItem != null)
            {
                _shopContext.Products.Remove(_shopContexItem);
                return true;
            }
            return false;
        }

        public Product GetData(int id)
        {
            var item = new Product();
            var _shopContexItem = _shopContext.Products.Find(id);
            if (_shopContexItem != null)
            {
                item = _shopContexItem;
            }
            return item;
        }

        public IEnumerable<Product> GetDataList()
        {
            var list = _shopContext.Products.Include(s => s.Brand).Include(s => s.ProductType).Include(s => s.HairType).Include(s => s.SupplyForProducts).ToList();

            return list;
        }

        public void Save()
        {
            _shopContext.SaveChanges();
        }

        public void Update(Product item)
        {
            _shopContext.Update(item);
        }
        /// <summary>
        /// 
        /// </summary>
        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _shopContext.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
