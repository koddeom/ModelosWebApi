using Controller_EF_Dapper_Repository_UnityOfWork.Domain.Database.Entities.Product;
using static Controller_EF_Dapper_Repository_UnityOfWork.Repository.Base.IGenericRepository;

namespace Controller_EF_Dapper_Repository_UnityOfWork.Repository.Repositories.Interfaces
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        //Metodos especificos
        IEnumerable<Product> GetAllSolds();
    }
}