namespace Controller_EF_Dapper_Repository_UnityOfWork.Endpoints.Products.DTO
{
    public record ProductSoldResponseDTO(
         Guid Id,
         string Name,
         int Amount
        );
}