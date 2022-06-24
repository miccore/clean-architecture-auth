using Miccore.CleanArchitecture.Auth.Core.Entities;

namespace Miccore.CleanArchitecture.Auth.Application.Responses.Role
{
    /// <summary>
    /// Role response
    /// </summary>
    public class RoleResponse : BaseEntity
    {
        public string? Name
        {
            get;
            set;
        }

    }
}