using Application.Parameters;


namespace Application.Features.Queries.GetAllClients
{
    public class GetAllClientParameters:RequestParameter
    {
        public string? FirstName { get; set;}
        public string? LastName { get; set;}
    }
}
