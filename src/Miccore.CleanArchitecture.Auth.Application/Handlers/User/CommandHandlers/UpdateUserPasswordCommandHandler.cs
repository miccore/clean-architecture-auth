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
    /// Update User Command Handler 
    /// </summary>
    public class UpdateUserPasswordCommandHandler : IRequestHandler<UpdateUserPasswordCommand, UserResponse>
    {
        private readonly IUserRepository _userRepository;

        public UpdateUserPasswordCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }


        /// <summary>
        /// User command Handle for updating element
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<UserResponse> Handle(UpdateUserPasswordCommand request, CancellationToken cancellationToken)
        {
            // check if user exist
            var user = await _userRepository.GetByIdAsync(request.Id);

            // check if passwords matched
            if(!BC.Verify(request.OldPassword, user.Password)){
                throw new NotFoundException(ExceptionEnum.USER_NOT_FOUND_OR_PASSWORD_INCORRECT.ToString());
            }
      
            // add async with the repository
            var updatedUser = await _userRepository.UpdatePasswordAsync(user, BC.HashPassword(request.NewPassword));

            //map with the response
            var userResponse = UserMapper.Mapper.Map<UserResponse>(updatedUser);

            // return response
            return userResponse;
        }
    }
}