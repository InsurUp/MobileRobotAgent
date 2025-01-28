namespace MobileRobotAgent.UI.Services.Abstract;

public interface ITokenService
{
    Task<string> GetTokenAsync();
    Task SaveTokenAsync(string token);
    Task ClearTokenAsync();
    bool ValidateToken(string token);
}