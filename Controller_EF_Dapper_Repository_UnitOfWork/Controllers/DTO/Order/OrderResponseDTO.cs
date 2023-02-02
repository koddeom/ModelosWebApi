namespace Controller_EF_Dapper_Repository_UnityOfWork.Endpoints.Orders.DTO
{
    public record OrderResponseDTO(Guid id,
                                   string ClientName,
                                   IEnumerable<OrderProductDTO> Products
                                );

    public record OrderProductDTO(Guid Id, String Name);
}