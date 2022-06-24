using MediatR;
using Miccore.CleanArchitecture.Auth.Application.Responses.User;

namespace Miccore.CleanArchitecture.Auth.Application.Queries.User
{
    public class GetUserByIdQuery : IRequest<UserResponse>
    {
        public int Id
        {
            get;
            set;
        }

        public GetUserByIdQuery(int Id)
        {
            this.Id = Id;
        }
    }
}