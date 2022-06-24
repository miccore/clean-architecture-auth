using FluentValidation;
using Miccore.CleanArchitecture.Auth.Application.Commands.User;
using Miccore.CleanArchitecture.Auth.Core.Enumerations;

namespace Miccore.CleanArchitecture.Auth.Api.Validators.User
{
    /// <summary>
    /// validator of update
    /// </summary>
    public class UpdateUserPasswordValidator : AbstractValidator<UpdateUserPasswordCommand>
    {
        public UpdateUserPasswordValidator(){
            
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("ID_"+ValidatorEnum.NOT_EMPTY.ToString())
                .NotNull().WithMessage("ID_"+ValidatorEnum.NOT_NULL.ToString());
            
            RuleFor(x => x.OldPassword)
                .NotEmpty().WithMessage("OLDPASSWORD_"+ValidatorEnum.NOT_EMPTY.ToString())
                .NotNull().WithMessage("OLDPASSWORD_"+ValidatorEnum.NOT_NULL.ToString());
            
            RuleFor(x => x.NewPassword)
                .NotEmpty().WithMessage("PASSWORD_"+ValidatorEnum.NOT_EMPTY.ToString())
                .NotNull().WithMessage("PASSWORD_"+ValidatorEnum.NOT_NULL.ToString())
                .MinimumLength(8).WithMessage(ValidatorEnum.PASSWORD_MINIMUM_LENGTH_8.ToString())
                .MaximumLength(16).WithMessage(ValidatorEnum.PASSWORD_MAX_LENGTH_16.ToString())
                .Matches(@"[A-Z]+").WithMessage(ValidatorEnum.PASSWORD_MUST_CONTAIN_ONE_UPPERCASE_LETTER.ToString())
                .Matches(@"[a-z]+").WithMessage(ValidatorEnum.PASSWORD_MUST_CONTAIN__ONE_LOWERCASE_LETTER.ToString())
                .Matches(@"[0-9]+").WithMessage(ValidatorEnum.PASSWORD_MUST_CONTAIN_ONE_NUMBER.ToString())
                .Matches(@"[\!\?\*\.]+").WithMessage(ValidatorEnum.PASSWORD_MUST_CONTAIN_AT_LEAST_SPECIAL_CHARS.ToString()+"_(!? *.)");

        }
    }
}