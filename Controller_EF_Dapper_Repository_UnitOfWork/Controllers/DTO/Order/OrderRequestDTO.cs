namespace Controller_EF_Dapper_Repository_UnityOfWork.Endpoints.Orders.DTO
{
    public record OrderRequestDTO(
        List<Guid> ProductListIds
    );
}