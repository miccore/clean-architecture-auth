using MediatR;
using Miccore.CleanArchitecture.Auth.Application.Commands.User;
using Miccore.CleanArchitecture.Auth.Application.Mappers;
using Miccore.CleanArchitecture.Auth.Application.Responses.User;
using Miccore.CleanArchitecture.Auth.Core.Enumerations;
using Miccore.CleanArchitecture.Auth.Core.Repositories;

namespace Miccore.CleanArchitecture.Auth.Application.Handlers.User.CommandHandlers
{
    /// <summary>
    /// Delete User Command Handler 
    /// </summary>
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, UserResponse>
    {
        private readonly IUserRepository _userRepository;

        public DeleteUserCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }


        /// <summary>
        /// User command Handle for delete element
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<UserResponse> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            // add async with the repository
            var deletedUser = await _userRepository.DeleteAsync(request.Id);

            //map with the response
            var userResponse = UserMapper.Mapper.Map<UserResponse>(deletedUser);

            // return response
            return userResponse;
        }
    }
}