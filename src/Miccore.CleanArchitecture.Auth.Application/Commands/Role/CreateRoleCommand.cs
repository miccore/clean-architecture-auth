using System.ComponentModel.DataAnnotations;
using MediatR;
using Miccore.CleanArchitecture.Auth.Application.Responses.Role;

namespace Miccore.CleanArchitecture.Auth.Application.Commands.Role
{
    /// <summary>
    ///  Auth Command request
    /// </summary>
    public class CreateRoleCommand : IRequest<RoleResponse>
    {
        [Required]
        public string? Name
        {
            get;
            set;
        }
    }

}