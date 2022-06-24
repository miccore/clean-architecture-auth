using FluentValidation;
using Miccore.CleanArchitecture.Auth.Application.Commands.Role;

namespace Miccore.CleanArchitecture.Auth.Api.Validators.Role
{
    /// <summary>
    /// validator of creation
    /// </summary>
    public class CreateRoleValidator : AbstractValidator<CreateRoleCommand>
    {
        public CreateRoleValidator(){
            
            RuleFor(x => x.Name)
                .NotEmpty()
                .NotNull();

        }
    }
}