using MediatR;
using Miccore.CleanArchitecture.Auth.Application.Responses.User;

namespace Miccore.CleanArchitecture.Auth.Application.Commands.User
{
    public class DeleteUserCommand : IRequest<UserResponse>
    {

        public int Id
        {
            get;
            set;
        }

        public DeleteUserCommand(int Id)
        {
            this.Id = Id;
        }
    }
}