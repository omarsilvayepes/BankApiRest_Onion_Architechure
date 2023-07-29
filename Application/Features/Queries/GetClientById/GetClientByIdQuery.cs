using Application.DTOs;
using Application.Interfaces;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Queries.GetClientById
{
    public  class GetClientByIdQuery:IRequest<Response<ClientDto>>
    {
        public int Id { get; set; }

        public class GetClientByIdQueryHandler : IRequestHandler<GetClientByIdQuery, Response<ClientDto>>
        {
            private readonly IRepositoryAsync<Client> repositoryAsync;
            private readonly IMapper mapper;

            public GetClientByIdQueryHandler(IRepositoryAsync<Client> repositoryAsync, IMapper mapper)
            {
                this.repositoryAsync = repositoryAsync;
                this.mapper = mapper;
            }
            public async Task<Response<ClientDto>> Handle(GetClientByIdQuery request, CancellationToken cancellationToken)
            {
                var clientFetched = await repositoryAsync.GetByIdAsync(request.Id);
                if (clientFetched == null)
                {
                    throw new KeyNotFoundException($"Register Not Found for id:{request.Id}");
                }
                else
                {
                   
                    var ClientDto=mapper.Map<ClientDto>(clientFetched);
                    return new Response<ClientDto>(ClientDto);
                }
            }
        }
    }
}
