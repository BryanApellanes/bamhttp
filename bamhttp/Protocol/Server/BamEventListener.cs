using System.Reflection;
using Bam.Net;

namespace Bam.Protocol.Server;

public class BamEventListener
{
    public BamEventListener()
    {
    }

    public BamEventListener(string eventName, Delegate eventHandler)
    {
        this.EventName = eventName;
        this.EventHandler = eventHandler;
    }

    public string EventName { get; set; }
    public Delegate EventHandler { get; set; }

    public void Listen(object eventSource, bool throwIfEventNotFound = false)
    {
        Type type = eventSource.GetType();
        EventInfo eventInfo = type.GetEvent(EventName);
        if (eventInfo != null)
        {
            eventInfo.AddEventHandler(eventSource, EventHandler);
            return;
        }

        if (throwIfEventNotFound)
        {
            throw new ArgumentException($"Specified event source does not define an event named {EventName}");
        }
    }
}