using Bam.Net;

namespace Bam.Protocol.Server;

public class BamRequestEventHandlers
{
    public BamRequestEventHandlers()
    {
        this.CreateContextStartedHandlers = new List<BamEventListener>();
        this.CreateContextCompleteHandlers = new List<BamEventListener>();
        this.ResolveUserStartedHandlers = new List<BamEventListener>();
        this.ResolveUserCompleteHandlers = new List<BamEventListener>();
        this.AuthorizeRequestStartedHandlers = new List<BamEventListener>();
        this.AuthorizeRequestCompleteHandlers = new List<BamEventListener>();
        this.ResolveSessionStateStartedHandlers = new List<BamEventListener>();
        this.ResolveSessionStateCompleteHandlers = new List<BamEventListener>();
        this.CreateResponseStartedHandlers = new List<BamEventListener>();
        this.CreateResponseCompleteHandlers = new List<BamEventListener>();
    }
    public List<BamEventListener> CreateContextStartedHandlers { get; }
    public List<BamEventListener> CreateContextCompleteHandlers { get; }
    public List<BamEventListener> ResolveUserStartedHandlers { get; }
    public List<BamEventListener> ResolveUserCompleteHandlers { get; }
    public List<BamEventListener> AuthorizeRequestStartedHandlers { get; }
    public List<BamEventListener> AuthorizeRequestCompleteHandlers { get; }
    public List<BamEventListener> ResolveSessionStateStartedHandlers { get; }
    public List<BamEventListener> ResolveSessionStateCompleteHandlers { get; }
    public List<BamEventListener> CreateResponseStartedHandlers { get; }
    public List<BamEventListener> CreateResponseCompleteHandlers { get; }

    public bool HasHandlers =>
        CreateContextStartedHandlers.Count > 0 ||
        CreateContextCompleteHandlers.Count > 0 ||
        ResolveUserStartedHandlers.Count > 0 ||
        ResolveUserCompleteHandlers.Count > 0 ||
        AuthorizeRequestStartedHandlers.Count > 0 ||
        AuthorizeRequestCompleteHandlers.Count > 0 ||
        ResolveSessionStateStartedHandlers.Count > 0 ||
        ResolveSessionStateCompleteHandlers.Count > 0 ||
        CreateResponseStartedHandlers.Count > 0 ||
        CreateResponseCompleteHandlers.Count > 0;

    internal void ListenTo(BamServer server)
    {
        List<BamEventListener> allEventListeners = new List<BamEventListener>();
        allEventListeners.AddRange(CreateContextStartedHandlers);
        allEventListeners.AddRange(CreateContextCompleteHandlers);
        allEventListeners.AddRange(ResolveUserStartedHandlers);
        allEventListeners.AddRange(ResolveUserCompleteHandlers);
        allEventListeners.AddRange(AuthorizeRequestStartedHandlers);
        allEventListeners.AddRange(AuthorizeRequestCompleteHandlers);
        allEventListeners.AddRange(ResolveSessionStateStartedHandlers);
        allEventListeners.AddRange(ResolveSessionStateCompleteHandlers);
        allEventListeners.AddRange(CreateResponseStartedHandlers);
        allEventListeners.AddRange(CreateResponseCompleteHandlers);

        foreach (BamEventListener bamEventListener in allEventListeners)
        {
            server.On(bamEventListener.EventName, bamEventListener.EventHandler);
        }
    }
}