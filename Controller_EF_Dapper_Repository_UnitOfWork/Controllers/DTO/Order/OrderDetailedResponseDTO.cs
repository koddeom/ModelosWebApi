namespace Controller_EF_Dapper_Repository_UnityOfWork.Endpoints.Orders.DTO
{
    public record OrderDetailedResponseDTO(string ClientId,
                                           string ClientName,
                                           IEnumerable<OrderProductDTO> Products
                                          );
}