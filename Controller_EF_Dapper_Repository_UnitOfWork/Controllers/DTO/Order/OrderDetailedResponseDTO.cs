namespace Controller_EF_Dapper_Repository_UnityOfWork.Endpoints.Orders.DTO
{
    public record OrderDetailedResponseDTO(Guid Id,
                                   string ClientName,
                                   IEnumerable<OrderProductDTO> Products
                                );
}