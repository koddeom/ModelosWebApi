namespace Controller_EF_Dapper_Repository_UnityOfWork.Endpoints.Products.DTO
{
    public record ProductResponseDTO(
        Guid Id,
        String Name,
        string Description,
        Decimal Price,
        bool Active
    );
}