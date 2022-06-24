using FluentValidation;
using Miccore.CleanArchitecture.Auth.Application.Commands.Role;
using Miccore.CleanArchitecture.Auth.Core.Enumerations;

namespace Miccore.CleanArchitecture.Auth.Api.Validators.Role
{
    /// <summary>
    /// validator of creation
    /// </summary>
    public class CreateRoleValidator : AbstractValidator<CreateRoleCommand>
    {
        public CreateRoleValidator(){
            
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("NAME_"+ValidatorEnum.NOT_EMPTY.ToString())
                .NotNull().WithMessage("NAME_"+ValidatorEnum.NOT_NULL.ToString());

        }
    }
}