using BLL.Entities;
using BLL.IRepositories;
using DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories
{
    public class EFBrandRepository : IRepository<Brand>
    {
        private readonly ShopContext _shopContext;

        public EFBrandRepository(ShopContext shopContext)
        {
            _shopContext = shopContext ?? throw new ArgumentNullException(nameof(shopContext));
        }

        public void Create(Brand item)
        {
            _shopContext.Brands.Add(item);
        }

        public bool Delete(int id)
        {
            var brand = _shopContext.Brands.Find(id);
            if (brand != null)
            {
                _shopContext.Brands.Remove(brand);
                return true;
            }
            return false;
        }

        public Brand GetData(int id)
        {
            var item = new Brand();
            var brand = _shopContext.Brands.Find(id);
            if (brand != null)
            {
                item = brand;
            }
            return item;
        }

        public IEnumerable<Brand> GetDataList()
        {
            var brands = _shopContext.Brands.ToList();

            return brands;
        }

        public void Save()
        {
            _shopContext.SaveChanges();
        }

        public void Update(Brand item)
        {
            var brand = _shopContext.Brands.FirstOrDefault(u => u.BrandID == item.BrandID);
            if(brand != null)
            {
                brand.BrandName = item.BrandName;
                _shopContext.Update(brand);
            }
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
