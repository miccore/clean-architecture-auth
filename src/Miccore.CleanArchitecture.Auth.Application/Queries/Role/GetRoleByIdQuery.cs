using MediatR;
using Miccore.CleanArchitecture.Auth.Application.Responses.Role;

namespace Miccore.CleanArchitecture.Auth.Application.Queries.Role
{
    public class GetRoleByIdQuery : IRequest<RoleResponse>
    {
        public int Id
        {
            get;
            set;
        }

        public GetRoleByIdQuery(int Id)
        {
            this.Id = Id;
        }
    }
}