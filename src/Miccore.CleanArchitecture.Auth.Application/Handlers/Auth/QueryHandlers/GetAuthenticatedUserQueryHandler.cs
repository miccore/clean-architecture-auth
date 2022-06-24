using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using MediatR;
using Miccore.CleanArchitecture.Auth.Application.Mappers;
using Miccore.CleanArchitecture.Auth.Application.Queries.Auth;
using Miccore.CleanArchitecture.Auth.Application.Responses.Auth;
using Miccore.CleanArchitecture.Auth.Application.Responses.User;
using Miccore.CleanArchitecture.Auth.Core.Enumerations;
using Miccore.CleanArchitecture.Auth.Core.Exceptions;
using Miccore.CleanArchitecture.Auth.Core.Repositories;

namespace Miccore.CleanArchitecture.Auth.Application.Handlers.Auth.QueryHandlers
{   
    /// <summary>
    /// get user by id
    /// </summary>
    public class GetAuthenticatedUserQueryHandler : IRequestHandler<GetAuthenticatedUserQuery, UserResponse>
    {
        private readonly IUserRepository _userRepository;
        public GetAuthenticatedUserQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        /// <summary>
        /// getting user by id query
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<UserResponse> Handle(GetAuthenticatedUserQuery request, CancellationToken cancellationToken)
        {   
            //get claims
            var handler = new JwtSecurityTokenHandler();
            var token = handler.ReadJwtToken(request.Token);
            var mobile = token.Claims.Where(x => x.Type == ClaimTypes.MobilePhone).FirstOrDefault();

            if(mobile is null || string.IsNullOrEmpty(mobile.Value)){
                throw new NotFoundException(ExceptionEnum.NOT_FOUND.ToString());
            }

            // get entity by id
            var entity = await _userRepository.GetSingleByPhoneAsync(mobile.Value);
          
            // mapping response
            var response = UserMapper.Mapper.Map<UserResponse>(entity);

            // return object
            return response;
        }
    }
}