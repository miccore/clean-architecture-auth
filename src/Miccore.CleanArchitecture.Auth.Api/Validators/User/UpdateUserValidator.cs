using FluentValidation;
using Miccore.CleanArchitecture.Auth.Application.Commands.User;

namespace Miccore.CleanArchitecture.Auth.Api.Validators.User
{
    /// <summary>
    /// validator of update
    /// </summary>
    public class UpdateUserValidator : AbstractValidator<UpdateUserCommand>
    {
        public UpdateUserValidator(){
            
            RuleFor(x => x.Id)
                .NotEmpty()
                .NotNull();

        }
    }
}