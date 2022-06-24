using MediatR;
using Miccore.CleanArchitecture.Auth.Application.Responses.User;

namespace Miccore.CleanArchitecture.Auth.Application.Queries.User
{
    public class GetUserByEmailQuery : IRequest<UserResponse>
    {
        public string Email
        {
            get;
            set;
        }

        public GetUserByEmailQuery(string Email)
        {
            this.Email = Email;
        }
    }
}