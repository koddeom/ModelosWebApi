using Controller_EF_Dapper_Repository_UnityOfWork.AppDomain.Database.BaseEntity;
using Flunt.Validations;

namespace Controller_EF_Dapper_Repository_UnityOfWork.Domain.Database.Entities.Product
{
    public class Order : Entity
    {
        public string ClientId { get; set; }
        public string ClientName { get; set; }
        public List<Product> Products { get; set; }
        public decimal Total { get; set; }

        private void Validate()
        {
            //validacao com flunt
            var contract = new Contract<Order>()
                .IsNotNullOrEmpty(ClientId, "ClientId", "Id é obrigatório")
                .IsNotNullOrEmpty(ClientName, "ClientName", "Nome é obrigatório");

            AddNotifications(contract);
        }

        public Order()
        {
            // use um construtor vazio sempre que operar com o ef
        }
    }
}