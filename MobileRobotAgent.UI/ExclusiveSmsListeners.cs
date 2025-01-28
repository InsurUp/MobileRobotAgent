namespace MobileRobotAgent.UI;

/// <summary>
/// This class handles the monitoring and processing of SMS messages.
/// While all incoming SMS messages are monitored, only those from predefined
/// insurance company senders (listed in SmsConstants.ValidSenders) are processed
/// and sent to the relevant APIs. This ensures that only SMS messages from these
/// trusted senders are utilized for further operations.
/// </summary>
public static class ExclusiveSmsListeners
{
    public static readonly Dictionary<string, int> ValidSenders = new()
    {
        { "+905358728627", 42 },
        { "+905303271409", 42 },
        { "ALLIANZ", 45 },
        { "ZURICH", 18 },
        { "ZURICH.", 18 },
        { "HEPIYI", 126 },
        { "AKSIGORTA", 100 },
        { "SOMPO", 61 },
        { "MgdburgerSg", 36 },
        { "QUICKSGORTA", 110 },
        { "RAY SIGORTA", 42 },
        { "GULFSIGORTA", 94 },
        { "UNICO", 17 },
        { "ANADOLUSIG", 7 },
        { ".AXA.", 95 },
        { "EUREKO A.S.", 2 },
    };
}