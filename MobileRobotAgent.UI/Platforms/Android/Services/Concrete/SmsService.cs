using Android.Content;
using MobileRobotAgent.UI.Services.Abstract;
using MobileRobotAgent.UI.Services.Models;

namespace MobileRobotAgent.UI.Platforms.Android.Services.Concrete;

public class SmsService : ISmsService
{
    private SmsReceiver _smsReceiver;
    private bool _isListening;

    public event EventHandler<SmsEventArgs> OnSmsReceived;

    public async Task<bool> RequestSmsPermissionAsync()
    {
        try
        {
            var status = await Permissions.CheckStatusAsync<Permissions.Sms>();
            if (status != PermissionStatus.Granted)
            {
                status = await Permissions.RequestAsync<Permissions.Sms>();
            }

            return status == PermissionStatus.Granted;
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"SMS Permission Error: {ex.Message}");
            return false;
        }
    }

    public Task StartListeningAsync()
    {
        if (_isListening) return Task.CompletedTask;

        _smsReceiver = new SmsReceiver();
        _smsReceiver.SmsReceived += (sender, args) =>
        {
            OnSmsReceived?.Invoke(this, args);
        };

        Platform.CurrentActivity.RegisterReceiver(
            _smsReceiver,
            new IntentFilter("android.provider.Telephony.SMS_RECEIVED")
        );

        _isListening = true;
        return Task.CompletedTask;
    }

    public Task StopListeningAsync()
    {
        if (!_isListening) return Task.CompletedTask;

        if (_smsReceiver != null)
        {
            Platform.CurrentActivity.UnregisterReceiver(_smsReceiver);
            _smsReceiver = null;
        }

        _isListening = false;
        return Task.CompletedTask;
    }
}