using AutoMapper;
using Miccore.CleanArchitecture.Auth.Application.Commands.Role;
using Miccore.CleanArchitecture.Auth.Application.Responses.Role;
using Miccore.Pagination.Model;

namespace Miccore.CleanArchitecture.Auth.Application.Mappers
{
    /// <summary>
    /// Role Mapping Profile map creation
    /// </summary>
    public class RoleMappingProfile : Profile
    {
        public RoleMappingProfile()
        {
            #region createmap

            // user response
            CreateMap<Miccore.CleanArchitecture.Auth.Core.Entities.Role, RoleResponse>().ReverseMap();
            // user create
            CreateMap<Miccore.CleanArchitecture.Auth.Core.Entities.Role, CreateRoleCommand>().ReverseMap();
            // user update
            CreateMap<Miccore.CleanArchitecture.Auth.Core.Entities.Role, UpdateRoleCommand>().ReverseMap();
            // user response pagination
            CreateMap<PaginationModel<Miccore.CleanArchitecture.Auth.Core.Entities.Role>, PaginationModel<RoleResponse>>().ReverseMap();

            #endregion
        }
    }
}