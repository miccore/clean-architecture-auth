namespace Miccore.CleanArchitecture.Auth.Core.Interfaces
{
    /// <summary>
    /// email sender interface
    /// </summary>
    public interface IEmailSender
    {
        Task SendEmailAsync(string to, string from, string subject, string body);
    }
}