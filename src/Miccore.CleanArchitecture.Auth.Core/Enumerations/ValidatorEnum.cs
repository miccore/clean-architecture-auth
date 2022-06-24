using System.ComponentModel;

namespace Miccore.CleanArchitecture.Auth.Core.Enumerations
{
    public enum ValidatorEnum
    {

        
        [Description("Not_EMPTY")]
        NOT_EMPTY,
        [Description("Not_NULL")]
        NOT_NULL,
        [Description("Your password length must be at least 8.")]
        PASSWORD_MINIMUM_LENGTH_8,
        [Description("Your password length must not exceed 16.")]
        PASSWORD_MAX_LENGTH_16,
        [Description("Your password must contain at least one uppercase letter.")]
        PASSWORD_MUST_CONTAIN_ONE_UPPERCASE_LETTER,
        [Description("Your password must contain at least one lowercase letter.")]
        PASSWORD_MUST_CONTAIN__ONE_LOWERCASE_LETTER,
        [Description("Your password must contain at least one number.")]
        PASSWORD_MUST_CONTAIN_ONE_NUMBER,
        [Description("Your password must contain at least one (!? *.).")]
        PASSWORD_MUST_CONTAIN_AT_LEAST_SPECIAL_CHARS,
    }
}