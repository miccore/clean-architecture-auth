using MediatR;
using Miccore.CleanArchitecture.Auth.Application.Responses.Auth;

namespace Miccore.CleanArchitecture.Auth.Application.Commands.Auth
{
    public class LoginCommand : IRequest<AuthResponse>
    {
        public string Phone { get; set; }
        public string Password { get; set; }
    }
}