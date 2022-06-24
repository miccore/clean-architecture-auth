using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using MediatR;
using Miccore.CleanArchitecture.Auth.Application.Responses.User;

namespace Miccore.CleanArchitecture.Auth.Application.Commands.User
{
    /// <summary>
    ///  Auth Command request
    /// </summary>
    public class CreateUserCommand : IRequest<UserResponse>
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

        [Required]
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

        [Required]
        public int RoleId
        {
            get;
            set;
        }
    }

}