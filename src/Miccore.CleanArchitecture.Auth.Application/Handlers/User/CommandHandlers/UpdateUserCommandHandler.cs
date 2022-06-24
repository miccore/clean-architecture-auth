using MediatR;
using Miccore.CleanArchitecture.Auth.Application.Commands.User;
using Miccore.CleanArchitecture.Auth.Application.Mappers;
using Miccore.CleanArchitecture.Auth.Application.Responses.User;
using Miccore.CleanArchitecture.Auth.Core.Enumerations;
using Miccore.CleanArchitecture.Auth.Core.Exceptions;
using Miccore.CleanArchitecture.Auth.Core.Repositories;

namespace Miccore.CleanArchitecture.Auth.Application.Handlers.User.CommandHandlers
{
    /// <summary>
    /// Update User Command Handler 
    /// </summary>
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, UserResponse>
    {
        private readonly IUserRepository _userRepository;

        public UpdateUserCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }


        /// <summary>
        /// User command Handle for updating element
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<UserResponse> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            // check if smple exist
            var user = await _userRepository.GetByIdAsync(request.Id);

            // map request with the entity
            var userEntity = UserMapper.Mapper.Map<Miccore.CleanArchitecture.Auth.Core.Entities.User>(request);

            // check if it's mapped correctly
            if(userEntity is null){
                throw new ApplicationException(ExceptionEnum.MAPPER_ISSUE.ToString());
            }

            // add async with the repository
            var updatedUser = await _userRepository.UpdateAsync(userEntity);

            //map with the response
            var userResponse = UserMapper.Mapper.Map<UserResponse>(updatedUser);

            // return response
            return userResponse;
        }
    }
}