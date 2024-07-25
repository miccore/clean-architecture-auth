namespace Miccore.CleanArchitecture.Auth.Application.Responses.Auth
{
    public class AuthResponse
    {
        public string Token
        {
            get;
            set;
        }
        public Core.Entities.User User
        {
            get;
            set;
        }
    }
}