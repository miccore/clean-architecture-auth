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
            CreateMap<Miccore.CleanArchitecture.Auth.Core.Entities.User, UserResponse>().ReverseMap();
            // user create
            CreateMap<Miccore.CleanArchitecture.Auth.Core.Entities.User, CreateUserCommand>().ReverseMap();
            // user update
            CreateMap<Miccore.CleanArchitecture.Auth.Core.Entities.User, UpdateUserCommand>().ReverseMap();
            // user response pagination
            CreateMap<PaginationModel<Miccore.CleanArchitecture.Auth.Core.Entities.User>, PaginationModel<UserResponse>>().ReverseMap();

            #endregion
        }
    }
}