using Application.DTOs.Users;
using Application.Interfaces;
using Application.Wrappers;
using MediatR;

namespace Application.Features.Users.Commands.RegisterUser
{
    public class RegisterUserCommand:IRequest<Response<string>>
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string Origin { get; set;}
    }

    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, Response<string>>
    {
        private readonly IAccountService accountService;

        public RegisterUserCommandHandler(IAccountService accountService)
        {
            this.accountService = accountService;
        }
        public async  Task<Response<string>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            return await accountService.RegisterAsync(new RegisterRequest 
            {
                Name = request.Name,
                Email=request.Email,
                ConfirmPassword = request.ConfirmPassword,
                UserName = request.UserName,
                Password = request.Password,
                LastName = request.LastName
            }, request.Origin);
        }
    }
}
