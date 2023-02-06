using Controller_EF_Dapper_Repository_UnityOfWork.Business.Interface;
using Controller_EF_Dapper_Repository_UnityOfWork.Business.Models;
using Controller_EF_Dapper_Repository_UnityOfWork.Domain.Database;
using Controller_EF_Dapper_Repository_UnityOfWork.Domain.Database.Entities.Product;
using Controller_EF_Dapper_Repository_UnityOfWork.Repository.Base;
using Controller_EF_Dapper_Repository_UnityOfWork.Repository.Repositories.Interfaces;

namespace Controller_EF_Dapper_Repository_UnityOfWork.Repository.Repositories
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        private readonly IServiceAllProductSold _serviceAllProductSold;

        public ProductRepository(ApplicationDbContext dbContext,
                                 IServiceAllProductSold serviceAllProductSold) : base(dbContext)
        {
            _serviceAllProductSold = serviceAllProductSold;
        }

        public Task<IEnumerable<ProductSold>> GetAllProductsSolds()
        {

            var productsSold = _serviceAllProductSold.Execute();

            return productsSold;

        }
    }
}