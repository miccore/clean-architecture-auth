using MediatR;
using Miccore.CleanArchitecture.Auth.Application.Mappers;
using Miccore.CleanArchitecture.Auth.Application.Queries.Role;
using Miccore.CleanArchitecture.Auth.Application.Responses.Role;
using Miccore.CleanArchitecture.Auth.Core.Repositories;
using Miccore.Pagination.Model;

namespace Miccore.CleanArchitecture.Auth.Application.Handlers.Role.QueryHandlers
{
    /// <summary>
    /// query handler
    /// </summary>
    public class GetAllRoleQueryHandler : IRequestHandler<GetAllRoleQuery, PaginationModel<RoleResponse>>
    {
        private readonly IRoleRepository _roleRepository;

        public GetAllRoleQueryHandler(IRoleRepository roleRepository)
        {
            this._roleRepository = roleRepository;
        }

        /// <summary>
        /// handle for getting all role paginated or not
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<PaginationModel<RoleResponse>> Handle(GetAllRoleQuery request, CancellationToken cancellationToken)
        {
            // get items
            var entities =  await _roleRepository.GetAllAsync(request.query);
            
            // mapping with response
            var responses = RoleMapper.Mapper.Map<PaginationModel<RoleResponse>>(entities);

            // return response
            return responses;
        }
    }
}