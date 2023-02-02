namespace Controller_EF_Dapper_Repository_UnityOfWork.Repository.Base
{
    public interface IGenericRepository
    {
        public interface IGenericRepository<T> where T : class
        {
            Task<T> Get(Guid id);

            Task<IEnumerable<T>> GetAll();

            Task Add(T entity);

            void Delete(T entity);

            void Update(T entity);
        }
    }
}