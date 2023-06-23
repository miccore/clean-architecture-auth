using MediatR;
using Miccore.CleanArchitecture.Auth.Application.Commands.Role;
using Miccore.CleanArchitecture.Auth.Application.Mappers;
using Miccore.CleanArchitecture.Auth.Application.Responses.Role;
using Miccore.CleanArchitecture.Auth.Core.Enumerations;
using Miccore.CleanArchitecture.Auth.Core.Exceptions;
using Miccore.CleanArchitecture.Auth.Core.Repositories;

namespace Miccore.CleanArchitecture.Auth.Application.Handlers.Role.CommandHandlers
{
    /// <summary>
    /// Update Role Command Handler 
    /// </summary>
    public class UpdateRoleCommandHandler : IRequestHandler<UpdateRoleCommand, RoleResponse>
    {
        private readonly IRoleRepository _roleRepository;

        public UpdateRoleCommandHandler(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }


        /// <summary>
        /// Role command Handle for updating element
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<RoleResponse> Handle(UpdateRoleCommand request, CancellationToken cancellationToken)
        {
            // map request with the entity
            var roleEntity = RoleMapper.Mapper.Map<Miccore.CleanArchitecture.Auth.Core.Entities.Role>(request);

            // check if it's mapped correctly
            if(roleEntity is null){
                throw new ApplicationException(ExceptionEnum.MAPPER_ISSUE.ToString());
            }

            // add async with the repository
            var updatedRole = await _roleRepository.UpdateAsync(roleEntity);

            //map with the response
            var roleResponse = RoleMapper.Mapper.Map<RoleResponse>(updatedRole);

            // return response
            return roleResponse;
        }
    }
}