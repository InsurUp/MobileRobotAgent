using MobileRobotAgent.UI.Services.Models;

namespace MobileRobotAgent.UI.Services.Abstract;

public interface ISmsService
{
    Task<bool> RequestSmsPermissionAsync();
    Task StartListeningAsync();
    Task StopListeningAsync();
    event EventHandler<SmsEventArgs> OnSmsReceived;
}