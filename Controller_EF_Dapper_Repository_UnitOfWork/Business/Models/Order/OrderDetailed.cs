using Controller_EF_Dapper_Repository_UnityOfWork.Endpoints.Orders.DTO;

namespace Controller_EF_Dapper_Repository_UnityOfWork.Business.Models.Product
{
    public class OrderDetailed
    {
        private object value;
        private IEnumerable<OrderProductDTO> orderProducts;

        public OrderDetailed(Guid id, string clientName, IEnumerable<OrderProductDTO> orderProducts)
        {
            this.Id = id;
            this.ClientName = clientName;
            this.orderProducts = orderProducts;
        }

        public Guid Id { get; set; }
        public string ClientName { get; set; }
        public IEnumerable<OrderProduct> Products { get; set; }
    }
}