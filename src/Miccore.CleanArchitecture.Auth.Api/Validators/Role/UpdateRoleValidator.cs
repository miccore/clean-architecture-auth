using FluentValidation;
using Miccore.CleanArchitecture.Auth.Application.Commands.Role;

namespace Miccore.CleanArchitecture.Auth.Api.Validators.Role
{
    /// <summary>
    /// validator of update
    /// </summary>
    public class UpdateRoleValidator : AbstractValidator<UpdateRoleCommand>
    {
        public UpdateRoleValidator(){
            
            RuleFor(x => x.Id)
                .NotEmpty()
                .NotNull();

            RuleFor(x => x.Name)
                .NotEmpty()
                .NotNull();

        }
    }
}