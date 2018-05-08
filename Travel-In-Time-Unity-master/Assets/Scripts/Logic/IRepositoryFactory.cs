using Assets.Scripts.DataAccess.Repository;

namespace Assets.Scripts.Logic
{
    //Generic Interface to create Repository
    public interface IRepositoryFactory<T>
    {
        IDataAccess<T> CreateRepository(string connectionString, string type);
    }
}
