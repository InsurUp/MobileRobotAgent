using MobileRobotAgent.UI.Services.Abstract;

namespace MobileRobotAgent.UI.Services.Concrete;

public class TokenService : ITokenService
{
    private const string TokenKey = "UserToken";
    private readonly ISecureStorage _secureStorage;

    public TokenService()
    {
        _secureStorage = SecureStorage.Default;
    }

    public async Task<string> GetTokenAsync()
    {
        return await _secureStorage.GetAsync(TokenKey);
    }

    public async Task SaveTokenAsync(string token)
    {
        if (!ValidateToken(token))
            throw new ArgumentException("Geçersiz token formatı");

        await _secureStorage.SetAsync(TokenKey, token);
    }

    public Task ClearTokenAsync()
    {
        SecureStorage.Default.Remove(TokenKey);
        return Task.CompletedTask;
    }

    public bool ValidateToken(string token)
    {
        if (string.IsNullOrWhiteSpace(token))
            return false;

        if (token.Length < 150 || token.Length > 512)
            return false;

        var base64Regex = new System.Text.RegularExpressions.Regex("^[a-zA-Z0-9+/=]+$");
        if (!base64Regex.IsMatch(token))
            return false;

        if (token.Contains(" ") || token.Contains("\n") || token.Contains("\r"))
            return false;

        try
        {
            Convert.FromBase64String(token);

            return true;
        }
        catch
        {
            return false;
        }
    }
}