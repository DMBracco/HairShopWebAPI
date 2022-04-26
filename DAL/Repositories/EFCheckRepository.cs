using BLL.Entities;
using BLL.IRepositories;
using DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories
{
    public class EFCheckRepository : IRepository<Check>
    {
        private readonly ShopContext _shopContext;

        public EFCheckRepository(ShopContext shopContext)
        {
            _shopContext = shopContext ?? throw new ArgumentNullException(nameof(shopContext));
        }

        public void Create(Check item)
        {
            _shopContext.Checks.Add(item);
        }

        public bool Delete(int id)
        {
            var _shopContexItem = _shopContext.Checks.Find(id);
            if (_shopContexItem != null)
            {
                _shopContext.Checks.Remove(_shopContexItem);
                return true;
            }
            return false;
        }

        public Check GetData(int id)
        {
            var item = new Check();
            var _shopContexItem = _shopContext.Checks.Find(id);
            if (_shopContexItem != null)
            {
                item = _shopContexItem;
            }
            return item;
        }

        public IEnumerable<Check> GetDataList()
        {
            var values = _shopContext.Checks.ToList();

            return values;
        }

        public void Save()
        {
            _shopContext.SaveChanges();
        }

        public void Update(Check item)
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
