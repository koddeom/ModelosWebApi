using WebApi_Minimal_EF_Dapper.Business.Models;

namespace WebApi_Minimal_EF_Dapper.Business.Interface
{
    public interface IServiceAllProductsSold
    {
        Task<IEnumerable<ProductSold>> Execute();
    }
}