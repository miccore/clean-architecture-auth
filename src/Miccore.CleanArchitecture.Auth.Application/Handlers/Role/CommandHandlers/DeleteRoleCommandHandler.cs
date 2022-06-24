using MediatR;
using Miccore.CleanArchitecture.Auth.Application.Commands.Role;
using Miccore.CleanArchitecture.Auth.Application.Mappers;
using Miccore.CleanArchitecture.Auth.Application.Responses.Role;
using Miccore.CleanArchitecture.Auth.Core.Enumerations;
using Miccore.CleanArchitecture.Auth.Core.Repositories;

namespace Miccore.CleanArchitecture.Auth.Application.Handlers.Role.CommandHandlers
{
    /// <summary>
    /// Delete Role Command Handler 
    /// </summary>
    public class DeleteRoleCommandHandler : IRequestHandler<DeleteRoleCommand, RoleResponse>
    {
        private readonly IRoleRepository _roleRepository;

        public DeleteRoleCommandHandler(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }


        /// <summary>
        /// Role command Handle for delete element
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<RoleResponse> Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
        {
            // add async with the repository
            var deletedRole = await _roleRepository.DeleteAsync(request.Id);

            //map with the response
            var roleResponse = RoleMapper.Mapper.Map<RoleResponse>(deletedRole);

            // return response
            return roleResponse;
        }
    }
}