using Application.DTOs;
using Application.Interfaces;
using Application.Specifications;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Queries.GetAllClients
{
    public class GetAllClientsQuery:IRequest<PagedResponse<List<ClientDto>>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public class GetAllClientsQueryHandler : IRequestHandler<GetAllClientsQuery, PagedResponse<List<ClientDto>>>
        {
            private readonly IRepositoryAsync<Client> repositoryAsync;
            private readonly IMapper mapper;

            public GetAllClientsQueryHandler(IRepositoryAsync<Client> repositoryAsync, IMapper mapper)
            {
                this.repositoryAsync = repositoryAsync;
                this.mapper = mapper;
            }
            public async Task<PagedResponse<List<ClientDto>>> Handle(GetAllClientsQuery request, CancellationToken cancellationToken)
            {
                var clientsList=await repositoryAsync.ListAsync(new PagedClientSpecification(request.PageSize,request.PageNumber,request.FirstName,request.LastName));
                var clientsListDto=mapper.Map<List<ClientDto>>(clientsList);

                return new PagedResponse<List<ClientDto>>(clientsListDto, request.PageNumber,request.PageSize);
            }

        }
    }
}
