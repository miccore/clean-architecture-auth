using MediatR;
using Miccore.CleanArchitecture.Auth.Application.Responses.User;

namespace Miccore.CleanArchitecture.Auth.Application.Queries.Auth
{
    public class GetAuthenticatedUserQuery : IRequest<UserResponse>
    {
        public string Token
        {
            get;
            set;
        }

        public GetAuthenticatedUserQuery(string Token)
        {
            this.Token = Token;
        }
    }
}