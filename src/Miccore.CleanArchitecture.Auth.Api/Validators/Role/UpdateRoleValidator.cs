using FluentValidation;
using Miccore.CleanArchitecture.Auth.Application.Commands.Role;
using Miccore.CleanArchitecture.Auth.Core.Enumerations;

namespace Miccore.CleanArchitecture.Auth.Api.Validators.Role
{
    /// <summary>
    /// validator of update
    /// </summary>
    public class UpdateRoleValidator : AbstractValidator<UpdateRoleCommand>
    {
        public UpdateRoleValidator(){
            
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("ID_"+ValidatorEnum.NOT_EMPTY.ToString())
                .NotNull().WithMessage("ID_"+ValidatorEnum.NOT_NULL.ToString());

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("NAME_"+ValidatorEnum.NOT_EMPTY.ToString())
                .NotNull().WithMessage("NAME_"+ValidatorEnum.NOT_NULL.ToString());

        }
    }
}