using Application.DTOs;
using Application.Interfaces;
using Application.Specifications;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System.Text;

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
            private readonly IDistributedCache distributedCache;

            public GetAllClientsQueryHandler(IRepositoryAsync<Client> repositoryAsync, IMapper mapper, IDistributedCache distributedCache)
            {
                this.repositoryAsync = repositoryAsync;
                this.mapper = mapper;
                this.distributedCache = distributedCache;
            }
            public async Task<PagedResponse<List<ClientDto>>> Handle(GetAllClientsQuery request, CancellationToken cancellationToken)
            {
                var cacheKey = $"ClientList_{request.PageNumber}_{request.PageSize}_{request.FirstName}_{request.LastName}";
                var clientsList=new List<Client>();
                var redisClientList = await distributedCache.GetAsync(cacheKey); //Get Cache :Tets directly in Redis console using Get + Key

                if (redisClientList!=null)
                {
                    clientsList = JsonConvert.DeserializeObject<List<Client>>(Encoding.UTF8.GetString(redisClientList));
                }
                else
                {
                    clientsList = await repositoryAsync.ListAsync(new PagedClientSpecification(request.PageSize, request.PageNumber, request.FirstName, request.LastName));
                    redisClientList = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(clientsList));

                    var options = new DistributedCacheEntryOptions()
                        .SetAbsoluteExpiration(DateTime.Now.AddMinutes(10)) // Time expire Cache
                        .SetSlidingExpiration(TimeSpan.FromMinutes(2)); // Time expire Cache

                    await distributedCache.SetAsync(cacheKey, redisClientList, options); // Save Cache
                  
                }
                
                var clientsListDto=mapper.Map<List<ClientDto>>(clientsList);

                return new PagedResponse<List<ClientDto>>(clientsListDto, request.PageNumber,request.PageSize);
            }

        }
    }
}
