using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Miccore.CleanArchitecture.Auth.Core.Entities
{
    /// <summary>
    /// Auth entity
    /// </summary>
    [Table("Users")]
    public class User : BaseEntity
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

        [PasswordPropertyText]
        public string? Password
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

        public string? RefreshToken
        {
            get;
            set;
        }

        public int RoleId
        {
            get;
            set;
        }

        public Role? Role
        {
            get;
        }

    }
}