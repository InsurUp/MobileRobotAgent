using Android.App;
using Android.Content;
using Android.Provider;
using MobileRobotAgent.UI.Services.Models;

namespace MobileRobotAgent.UI.Platforms.Android.Services.Concrete;

[BroadcastReceiver(Enabled = true, Exported = true, Permission = "android.permission.BROADCAST_SMS")]
[IntentFilter(new[] { "android.provider.Telephony.SMS_RECEIVED" })]
public class SmsReceiver : BroadcastReceiver
{
    public event EventHandler<SmsEventArgs> SmsReceived;

    public override void OnReceive(Context context, Intent intent)
    {
        if (intent.Action != "android.provider.Telephony.SMS_RECEIVED") return;

        var messages = Telephony.Sms.Intents.GetMessagesFromIntent(intent);

        foreach (var message in messages)
        {
            var args = new SmsEventArgs
            {
                Sender = message.DisplayOriginatingAddress,
                Message = message.DisplayMessageBody,
                ReceivedAt = DateTime.Now
            };

            SmsReceived?.Invoke(this, args);
        }
    }
}