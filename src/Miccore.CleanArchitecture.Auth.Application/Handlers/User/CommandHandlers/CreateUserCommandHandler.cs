using MediatR;
using Miccore.CleanArchitecture.Auth.Application.Commands.User;
using Miccore.CleanArchitecture.Auth.Application.Mappers;
using Miccore.CleanArchitecture.Auth.Application.Responses.User;
using Miccore.CleanArchitecture.Auth.Core.Enumerations;
using Miccore.CleanArchitecture.Auth.Core.Exceptions;
using Miccore.CleanArchitecture.Auth.Core.Repositories;
using BC = BCrypt.Net.BCrypt;

namespace Miccore.CleanArchitecture.Auth.Application.Handlers.User.CommandHandlers
{
    /// <summary>
    /// Create User Command Handler 
    /// </summary>
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, UserResponse>
    {
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;

        public CreateUserCommandHandler(
            IUserRepository userRepository,
            IRoleRepository roleRepository
        )
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
        }


        /// <summary>
        /// User command Handle for adding new element
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<UserResponse> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            // map request with the entity
            var userEntity = UserMapper.Mapper.Map<Miccore.CleanArchitecture.Auth.Core.Entities.User>(request);
            
            // check if it's mapped correctly
            if(userEntity is null){
                throw new ApplicationException(ExceptionEnum.MAPPER_ISSUE.ToString());
            }

            // check if role exist
            var role = await _roleRepository.GetByIdAsync(userEntity.RoleId);
            if (role is null){
                throw new NotFoundException(ExceptionEnum.ROLE_NOT_FOUND.ToString());
            }

            // encrypt password
            userEntity.Password = BC.HashPassword(userEntity.Password);

            // add async with the repository
            var addedUser = await _userRepository.AddAsync(userEntity);

            //map with the response
            var userResponse = UserMapper.Mapper.Map<UserResponse>(addedUser);

            // return response
            return userResponse;
        }
    }
}