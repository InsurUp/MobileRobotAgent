namespace MobileRobotAgent.UI.Services.Models;

public class SmsEventArgs : EventArgs
{
    public string Sender { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
    public DateTime ReceivedAt { get; set; }
}