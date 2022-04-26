using System.Linq.Expressions;

namespace BLL.IRepositories
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        IEnumerable<TEntity> GetList(
            Expression<Func<TEntity, bool>>? filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
            string includeProperties = "");
        IEnumerable<TEntity> GetList();
        TEntity? GetByIdOrNULL(object id);
        void Created(TEntity entity);
        bool Delete(object id);
        void Delete(TEntity entityToDelete);
        void Update(TEntity entityToUpdate);
    }
}
