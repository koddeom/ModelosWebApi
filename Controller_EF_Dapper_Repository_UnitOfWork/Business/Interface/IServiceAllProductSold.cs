using Controller_EF_Dapper_Repository_UnityOfWork.Business.Models;

namespace Controller_EF_Dapper_Repository_UnityOfWork.Business.Interface
{
    public interface IServiceAllProductSold
    {
        Task<IEnumerable<ProductSold>> Execute();
    }
}