using MediatR;
using Miccore.CleanArchitecture.Auth.Application.Responses.User;

namespace Miccore.CleanArchitecture.Auth.Application.Queries.User
{
    public class GetUserByPhoneQuery : IRequest<UserResponse>
    {
        public string Phone { get; set; }
        public GetUserByPhoneQuery(string Phone)
        {
            this.Phone = Phone;
        }
    }
}