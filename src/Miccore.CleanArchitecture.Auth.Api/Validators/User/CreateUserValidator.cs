using FluentValidation;
using Miccore.CleanArchitecture.Auth.Application.Commands.User;
using Miccore.CleanArchitecture.Auth.Core.Enumerations;

namespace Miccore.CleanArchitecture.Auth.Api.Validators.User
{
    /// <summary>
    /// validator of creation
    /// </summary>
    public class CreateUserValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserValidator(){
            
            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("FIRSTNAME_"+ValidatorEnum.NOT_EMPTY.ToString())
                .NotNull().WithMessage("FIRSTNAME_"+ValidatorEnum.NOT_NULL.ToString());
            
            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("PASSWORD_"+ValidatorEnum.NOT_EMPTY.ToString())
                .NotNull().WithMessage("PASSWORD_"+ValidatorEnum.NOT_NULL.ToString())
                .MinimumLength(8).WithMessage(ValidatorEnum.PASSWORD_MINIMUM_LENGTH_8.ToString())
                .Matches(@"[A-Z]+").WithMessage(ValidatorEnum.PASSWORD_MUST_CONTAIN_ONE_UPPERCASE_LETTER.ToString())
                .Matches(@"[a-z]+").WithMessage(ValidatorEnum.PASSWORD_MUST_CONTAIN__ONE_LOWERCASE_LETTER.ToString())
                .Matches(@"[0-9]+").WithMessage(ValidatorEnum.PASSWORD_MUST_CONTAIN_ONE_NUMBER.ToString())
                .Matches(@"[^A-Za-z0-9]+").WithMessage(ValidatorEnum.PASSWORD_MUST_CONTAIN_AT_LEAST_SPECIAL_CHARS.ToString());
            
            RuleFor(x => x.RoleId)
                .NotEmpty()
                .NotNull();

        }
    }
}
