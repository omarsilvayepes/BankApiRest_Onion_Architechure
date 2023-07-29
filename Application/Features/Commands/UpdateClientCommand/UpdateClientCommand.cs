using Application.Interfaces;
using Application.Wrappers;
using Domain.Entities;
using MediatR;


namespace Application.Features.Commands.UpdateClientCommand
{
    public class UpdateClientCommand : IRequest<Response<int>>
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime Birthday { get; set; }
        public string Telephone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
    }

    public class UpdateClientCommandHandler : IRequestHandler<UpdateClientCommand, Response<int>>
    {
        private readonly IRepositoryAsync<Client> repositoryAsync;

        public UpdateClientCommandHandler(IRepositoryAsync<Client> repositoryAsync)
        {
            this.repositoryAsync = repositoryAsync;
        }
        public async Task<Response<int>> Handle(UpdateClientCommand request, CancellationToken cancellationToken)
        {
            var clientFetched = await repositoryAsync.GetByIdAsync(request.Id);
            if (clientFetched == null)
            {
                throw new KeyNotFoundException($"Register Not Found for id: {request.Id}");
            }
            else
            {
                clientFetched.FirstName = request.FirstName;
                clientFetched.LastName = request.LastName;
                clientFetched.Email = request.Email;
                clientFetched.Address = request.Address;
                clientFetched.Birthday = request.Birthday;
                clientFetched.Telephone = request.Telephone;

                await repositoryAsync.UpdateAsync(clientFetched);
                return new Response<int>(clientFetched.Id);
            }
        }
    }
}
