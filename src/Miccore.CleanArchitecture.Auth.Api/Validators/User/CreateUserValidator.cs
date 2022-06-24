using FluentValidation;
using Miccore.CleanArchitecture.Auth.Application.Commands.User;

namespace Miccore.CleanArchitecture.Auth.Api.Validators.User
{
    /// <summary>
    /// validator of creation
    /// </summary>
    public class CreateUserValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserValidator(){
            
            RuleFor(x => x.FirstName)
                .NotEmpty()
                .NotNull();
            
            RuleFor(x => x.Password)
                .NotEmpty()
                .NotNull();
            
            RuleFor(x => x.RoleId)
                .NotEmpty()
                .NotNull();

        }
    }
}