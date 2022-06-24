using FluentValidation;
using Miccore.CleanArchitecture.Auth.Application.Commands.Auth;
using Miccore.CleanArchitecture.Auth.Application.Commands.User;
using Miccore.CleanArchitecture.Auth.Core.Enumerations;

namespace Miccore.CleanArchitecture.Auth.Api.Validators.User
{
    /// <summary>
    /// validator of creation
    /// </summary>
    public class LoginValidator : AbstractValidator<LoginCommand>
    {
        public LoginValidator(){
            
            RuleFor(x => x.Phone)
                .NotEmpty().WithMessage("PHONE_"+ValidatorEnum.NOT_EMPTY.ToString())
                .NotNull().WithMessage("PHONE_"+ValidatorEnum.NOT_NULL.ToString());
            
            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("PASSWORD_"+ValidatorEnum.NOT_EMPTY.ToString())
                .NotNull().WithMessage("PASSWORD_"+ValidatorEnum.NOT_NULL.ToString());

        }
    }
}