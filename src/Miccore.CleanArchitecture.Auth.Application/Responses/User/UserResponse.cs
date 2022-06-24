using System.ComponentModel.DataAnnotations;
using Miccore.CleanArchitecture.Auth.Core.Entities;

namespace Miccore.CleanArchitecture.Auth.Application.Responses.User
{
    /// <summary>
    /// User response
    /// </summary>
    public class UserResponse : BaseEntity
    {
         [Required]
        public string? FirstName
        {
            get;
            set;
        }

        public string? LastName
        {
            get;
            set;
        }
        [Phone]
        public string? Phone
        {
            get;
            set;
        }

        [EmailAddress]
        public string? Email
        {
            get;
            set;
        }

        public string? Address
        {
            get;
            set;
        }

        public int RoleId
        {
            get;
            set;
        }

        public Miccore.CleanArchitecture.Auth.Core.Entities.Role? Role
        {
            get;
        }
    }
}