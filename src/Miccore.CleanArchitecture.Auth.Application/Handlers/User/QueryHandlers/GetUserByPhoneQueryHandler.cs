using MediatR;
using Miccore.CleanArchitecture.Auth.Application.Mappers;
using Miccore.CleanArchitecture.Auth.Application.Queries.User;
using Miccore.CleanArchitecture.Auth.Application.Responses.User;
using Miccore.CleanArchitecture.Auth.Core.Enumerations;
using Miccore.CleanArchitecture.Auth.Core.Exceptions;
using Miccore.CleanArchitecture.Auth.Core.Repositories;

namespace Miccore.CleanArchitecture.Auth.Application.Handlers.User.QueryHandlers
{   
    /// <summary>
    /// get user by id
    /// </summary>
    public class GetUserByPhoneQueryHandler : IRequestHandler<GetUserByPhoneQuery, UserResponse>
    {
        private readonly IUserRepository _userRepository;
        public GetUserByPhoneQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        /// <summary>
        /// getting user by id query
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<UserResponse> Handle(GetUserByPhoneQuery request, CancellationToken cancellationToken)
        {
            // get entity by id
            var entity = await _userRepository.GetSingleByPhoneAsync(request.Phone);
          
            // mapping response
            var response = UserMapper.Mapper.Map<UserResponse>(entity);

            // return object
            return response;
        }
    }
}