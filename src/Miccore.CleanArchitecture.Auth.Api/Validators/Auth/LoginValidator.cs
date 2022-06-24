using FluentValidation;
using Miccore.CleanArchitecture.Auth.Application.Commands.Auth;
using Miccore.CleanArchitecture.Auth.Application.Commands.User;

namespace Miccore.CleanArchitecture.Auth.Api.Validators.User
{
    /// <summary>
    /// validator of creation
    /// </summary>
    public class LoginValidator : AbstractValidator<LoginCommand>
    {
        public LoginValidator(){
            
            RuleFor(x => x.Phone)
                .NotEmpty()
                .NotNull();
            
            RuleFor(x => x.Password)
                .NotEmpty()
                .NotNull();

        }
    }
}