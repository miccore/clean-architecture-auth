using System.ComponentModel.DataAnnotations;
using MediatR;
using Miccore.CleanArchitecture.Auth.Application.Responses.User;

namespace Miccore.CleanArchitecture.Auth.Application.Commands.User
{
    /// <summary>
    ///  Auth Command request
    /// </summary>
    public class UpdateUserPasswordCommand : IRequest<UserResponse>
    {
        public int Id { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
        
    }

}