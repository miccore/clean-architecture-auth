using FluentValidation;
using Miccore.CleanArchitecture.Auth.Application.Commands.User;
using Miccore.CleanArchitecture.Auth.Core.Enumerations;

namespace Miccore.CleanArchitecture.Auth.Api.Validators.User
{
    /// <summary>
    /// validator of update
    /// </summary>
    public class UpdateUserValidator : AbstractValidator<UpdateUserCommand>
    {
        public UpdateUserValidator(){
            
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("ID_"+ValidatorEnum.NOT_EMPTY.ToString())
                .NotNull().WithMessage("ID_"+ValidatorEnum.NOT_NULL.ToString());

        }
    }
}