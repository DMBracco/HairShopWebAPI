using BLL.Entities;

namespace BLL.IRepositories
{
    public interface IBaseRepository : IDisposable
    {
        IEnumerable<Supply> GetSupplyListWithSupplier();
        IEnumerable<Product> GetListOfSupply();
    }
}
