using MediatR;
using Miccore.CleanArchitecture.Auth.Application.Responses.Auth;

namespace Miccore.CleanArchitecture.Auth.Application.Commands.Auth
{
    public class RefreshTokenCommand : IRequest<AuthResponse>
    {
        public RefreshTokenCommand(string refreshToken)
        {
            this.RefreshToken = RefreshToken;
        }

        public string RefreshToken { get; set; }
        
    }
}