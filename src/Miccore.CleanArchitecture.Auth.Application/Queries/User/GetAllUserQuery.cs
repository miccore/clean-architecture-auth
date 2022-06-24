using MediatR;
using Miccore.CleanArchitecture.Auth.Application.Responses.User;
using Miccore.Pagination.Model;

namespace Miccore.CleanArchitecture.Auth.Application.Queries.User
{
    /// <summary>
    /// User query model
    /// </summary>
    public class GetAllUserQuery : IRequest<PaginationModel<UserResponse>>
    {
        public PaginationQuery query
        {
            get;
            set;
        }

        public GetAllUserQuery(PaginationQuery query)
        {
            this.query = query;
        }
    }
}