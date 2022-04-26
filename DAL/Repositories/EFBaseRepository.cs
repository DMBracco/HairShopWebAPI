using BLL.Entities;
using BLL.IRepositories;
using DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories
{
    public class EFBaseRepository : IBaseRepository
    {
        private readonly ShopContext _shopContext;

        public EFBaseRepository(ShopContext shopContext)
        {
            _shopContext = shopContext ?? throw new ArgumentNullException(nameof(shopContext));
        }

        public IEnumerable<Supply> GetSupplyListWithSupplier()
        {
            var list = _shopContext.Supplies.Include(s=>s.Supplier).ToList();

            return list;
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

        public IEnumerable<Product> GetListOfSupply()
        {
            //var list = _shopContext.Products.Include(s => s.SupplyForProducts).ToList();
            var list = from p in _shopContext.Products
            join o in _shopContext.Supplies on p.SupplyForProducts equals o.SupplyForProducts 
            into prodGroup select new Product { 
                ProductID = p.ProductID,

            };

            return list;
        }
    }
}
