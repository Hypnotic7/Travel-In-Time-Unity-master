namespace Assets.Scripts.DataAccess.Repository
{
    //Generic interface for Data access
    public interface IDataAccess<T>
    {
        string CollectionName { set; get; }
        void Add(T entity);
        void Remove(T entity);
        void Modify(T entity);

        T Fetch(T property);
    }
}
