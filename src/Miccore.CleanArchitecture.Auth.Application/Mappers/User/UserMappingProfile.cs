using AutoMapper;
using Miccore.CleanArchitecture.Auth.Application.Commands.User;
using Miccore.CleanArchitecture.Auth.Application.Responses.User;
using Miccore.Pagination.Model;

namespace Miccore.CleanArchitecture.Auth.Application.Mappers
{
    /// <summary>
    /// User Mapping Profile map creation
    /// </summary>
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            #region createmap

            // user response
            CreateMap<Core.Entities.User, UserResponse>().ReverseMap();
            // user create
            CreateMap<Core.Entities.User, CreateUserCommand>().ReverseMap();
            // user update
            CreateMap<Core.Entities.User, UpdateUserCommand>().ReverseMap();
            // user password update
            CreateMap<Core.Entities.User, UpdateUserPasswordCommand>().ReverseMap();
            // user response pagination
            CreateMap<PaginationModel<Core.Entities.User>, PaginationModel<UserResponse>>().ReverseMap();

            #endregion
        }
    }
}