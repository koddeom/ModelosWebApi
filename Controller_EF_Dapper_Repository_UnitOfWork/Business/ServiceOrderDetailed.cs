using Controller_EF_Dapper_Repository_UnityOfWork.AppDomain.Database.Entities;
using Controller_EF_Dapper_Repository_UnityOfWork.AppDomain.Extensions.ErroDetailedExtension;
using Controller_EF_Dapper_Repository_UnityOfWork.Business.Interface;
using Controller_EF_Dapper_Repository_UnityOfWork.Business.Models.Product;
using Controller_EF_Dapper_Repository_UnityOfWork.Domain.Database;
using Controller_EF_Dapper_Repository_UnityOfWork.Endpoints.Orders.DTO;
using Microsoft.AspNetCore.Mvc;

namespace Controller_EF_Dapper_Repository_UnityOfWork.Business
{
    public class ServiceOrderDetailed : IServiceOrderDetailed
    {
        private readonly ApplicationDbContext _dbContext;

        public ServiceOrderDetailed()
        {
        }

        public ServiceOrderDetailed(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<OrderDetailed> Get(Guid id)
        {
            //01 Recupero a Order
            var order = _dbContext.Orders.FirstOrDefault(o => o.Id == id);  

            //02 Recupero os produtos da order
            var orderProducts = order.Products.Select(p => new OrderProductDTO(p.Id, p.Name));


            //03 Monto o objeto composto com os dados da order e a lista de produtos
            var orderDetailed = new OrderDetailed(order.Id, null, orderProducts);

            return orderDetailed;
        }

        public async Task<ObjectResult> SaveOrder(List<Guid> orderProductsId, OrderBuyer orderBuyer)
        {
            List<Product> orderProducts = new List<Product>();

            //Recupero os produtos do banco para garantir consistencia dos dados
            if (orderProductsId.Any())
                orderProducts = _dbContext.Products.Where(p => orderProductsId.Contains(p.Id)).ToList();

            if (orderProducts == null)
                return new ObjectResult(Results.NotFound());

            //Total gasto
            decimal total = 0;
            foreach (var product in orderProducts)
            {
                total += product.Price;
            }

            var order = new Order();

            order.ClientId = orderBuyer.Id;
            order.ClientName = orderBuyer.Name;
            order.Products = orderProducts;
            order.Total = total;

            if (!order.IsValid)
            {
                return new ObjectResult(Results.ValidationProblem(order.Notifications.ConvertToErrorDetails()));
            }

            await _dbContext.Orders.AddAsync(order);
            await _dbContext.SaveChangesAsync();

            return new ObjectResult(Results.Created($"/orders/{order.Id}", order.Id));
        }
    }
}