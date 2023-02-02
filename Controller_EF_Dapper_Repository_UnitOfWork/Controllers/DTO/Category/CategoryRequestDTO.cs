namespace Controller_EF_Dapper_Repository_UnityOfWork.Endpoints.Categories.DTO
{
    public record CategoryRequestDTO(
        String Name,
        bool Active
    );
}