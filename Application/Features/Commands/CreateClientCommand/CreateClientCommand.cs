using Application.Interfaces;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Commands.CreateClientCommand
{
    public class CreateClientCommand:IRequest<Response<int>>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime Birthday { get; set; }
        public string Telephone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
    }

    public class CreateClientCommandHandler : IRequestHandler<CreateClientCommand, Response<int>>
    {
        private readonly IRepositoryAsync<Client> repositoryAsync;
        private readonly IMapper mapper;

        public CreateClientCommandHandler(IRepositoryAsync<Client> repositoryAsync,IMapper mapper)
        {
            this.repositoryAsync = repositoryAsync;
            this.mapper = mapper;
        }
        public async Task<Response<int>> Handle(CreateClientCommand request, CancellationToken cancellationToken)
        {
            var newRecord = mapper.Map<Client>(request);
            var data = await repositoryAsync.AddAsync(newRecord);
            return new Response<int>(data.Id);
        }
    }
}
