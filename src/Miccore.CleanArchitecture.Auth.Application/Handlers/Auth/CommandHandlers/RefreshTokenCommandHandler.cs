using MediatR;
using Miccore.CleanArchitecture.Auth.Application.Commands.Auth;
using Miccore.CleanArchitecture.Auth.Application.Mappers;
using Miccore.CleanArchitecture.Auth.Application.Responses.Auth;
using Miccore.CleanArchitecture.Auth.Core.Enumerations;
using Miccore.CleanArchitecture.Auth.Core.Exceptions;
using Miccore.CleanArchitecture.Auth.Core.Repositories;
using Miccore.CleanArchitecture.Auth.Core.Utils;
using BC = BCrypt.Net.BCrypt;

namespace Miccore.CleanArchitecture.Auth.Application.Handlers.Auth.CommandHandlers
{
    /// <summary>
    /// Create Auth Command Handler 
    /// </summary>
    public class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, AuthResponse>
    {
        private readonly IUserRepository _userRepository;

        public RefreshTokenCommandHandler(
            IUserRepository userRepository
        )
        {
            _userRepository = userRepository;
        }


        /// <summary>
        /// Auth command Handle for adding new element
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<AuthResponse> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            // get user and check if it exist
            var login = await _userRepository.GetSingleByRefreshTokenAsync(request.RefreshToken);

            // generate token
            var token = AuthenticationUtils.GenerateToken(login);

            // generate refresh token
            var refresh = AuthenticationUtils.GenerateRefreshToken();

            //update refresh token
            login.RefreshToken = refresh;
            var updatedUser = await _userRepository.UpdateRefreshTokenAsync(login);

            // create response
            var response = new AuthResponse();
            response.Token = token;
            response.User = updatedUser;

            // return response
            return response;
        }
    }
}