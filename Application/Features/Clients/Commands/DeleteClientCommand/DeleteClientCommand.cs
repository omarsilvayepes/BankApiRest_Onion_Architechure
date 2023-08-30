using Application.Interfaces;
using Application.Wrappers;
using Domain.Entities;
using MediatR;


namespace Application.Features.Commands.DeleteClientCommand
{
    public class DeleteClientCommand : IRequest<Response<int>>
    {
        public int Id { get; set; }
    }

    public class DeleteClientCommandHandler : IRequestHandler<DeleteClientCommand, Response<int>>
    {
        private readonly IRepositoryAsync<Client> repositoryAsync;


        public DeleteClientCommandHandler(IRepositoryAsync<Client> repositoryAsync)
        {
            this.repositoryAsync = repositoryAsync;
        }
        public async Task<Response<int>> Handle(DeleteClientCommand request, CancellationToken cancellationToken)
        {
            var clientFetched = await repositoryAsync.GetByIdAsync(request.Id);
            if (clientFetched == null)
            {
                throw new KeyNotFoundException($"Register Not Found for id:{request.Id}");
            }
            else
            {
                await repositoryAsync.DeleteAsync(clientFetched);
                return new Response<int>(clientFetched.Id);
            }
        }
    }
}
