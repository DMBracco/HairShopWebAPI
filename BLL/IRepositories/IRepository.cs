namespace BLL.IRepositories
{
    public interface IRepository<T> : IDisposable where T : class
    {
        IEnumerable<T> GetDataList(); // получение всех объектов
        T GetData(int id); // получение одного объекта по id
        void Create(T item); // создание объекта
        void Update(T item); // обновление объекта
        bool Delete(int id); // удаление объекта по id
        void Save();  // сохранение изменений
    }
}
