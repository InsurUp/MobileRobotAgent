namespace MobileRobotAgent.UI.Services.Abstract;

public interface ISmsProcessingService
{
    Task StartProcessingAsync();
    Task StopProcessingAsync();
    (bool IsValid, int CompanyId) ValidateSender(string sender);
    Task ProcessSmsAsync(string sender, string message);
}