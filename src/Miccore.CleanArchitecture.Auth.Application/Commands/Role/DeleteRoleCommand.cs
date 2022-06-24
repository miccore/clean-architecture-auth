using MediatR;
using Miccore.CleanArchitecture.Auth.Application.Responses.Role;

namespace Miccore.CleanArchitecture.Auth.Application.Commands.Role
{
    public class DeleteRoleCommand : IRequest<RoleResponse>
    {

        public int Id
        {
            get;
            set;
        }

        public DeleteRoleCommand(int Id)
        {
            this.Id = Id;
        }
    }
}