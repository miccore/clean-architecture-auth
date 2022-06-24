using MediatR;
using Miccore.CleanArchitecture.Auth.Application.Responses.Role;
using Miccore.Pagination.Model;

namespace Miccore.CleanArchitecture.Auth.Application.Queries.Role
{
    /// <summary>
    /// Role query model
    /// </summary>
    public class GetAllRoleQuery : IRequest<PaginationModel<RoleResponse>>
    {
        public PaginationQuery query
        {
            get;
            set;
        }

        public GetAllRoleQuery(PaginationQuery query)
        {
            this.query = query;
        }
    }
}