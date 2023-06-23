using System.ComponentModel;

namespace Miccore.CleanArchitecture.Auth.Core.Enumerations
{
    public enum ExceptionEnum
    {
        [Description("Coockie not found")]
        COOCKIE_NOT_FOUND,
        
        [Description("Header not found")]
        HEADER_NOT_FOUND,

        [Description("Mapper Issue")]
        MAPPER_ISSUE,

        #region enums

        [Description("not found")]
        NOT_FOUND,

        [Description("Role not found")]
        ROLE_NOT_FOUND,

        [Description("USER not found")]
        USER_NOT_FOUND,

        [Description("USER not found or password incorrect")]
        USER_NOT_FOUND_OR_PASSWORD_INCORRECT,

        #endregion
    }
}
