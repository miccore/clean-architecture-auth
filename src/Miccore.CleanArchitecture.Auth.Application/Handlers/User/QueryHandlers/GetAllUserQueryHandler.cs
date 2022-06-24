using MediatR;
using Miccore.CleanArchitecture.Auth.Application.Mappers;
using Miccore.CleanArchitecture.Auth.Application.Queries.User;
using Miccore.CleanArchitecture.Auth.Application.Responses.User;
using Miccore.CleanArchitecture.Auth.Core.Repositories;
using Miccore.Pagination.Model;

namespace Miccore.CleanArchitecture.Auth.Application.Handlers.User.QueryHandlers
{
    /// <summary>
    /// query handler
    /// </summary>
    public class GetAllUserQueryHandler : IRequestHandler<GetAllUserQuery, PaginationModel<UserResponse>>
    {
        private readonly IUserRepository _userRepository;

        public GetAllUserQueryHandler(IUserRepository userRepository)
        {
            this._userRepository = userRepository;
        }

        /// <summary>
        /// handle for getting all user paginated or not
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<PaginationModel<UserResponse>> Handle(GetAllUserQuery request, CancellationToken cancellationToken)
        {
            // get items
            var entities =  await _userRepository.GetAllAsync(request.query);
            
            // mapping with response
            var responses = UserMapper.Mapper.Map<PaginationModel<UserResponse>>(entities);

            // return response
            return responses;
        }
    }
}