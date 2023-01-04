using Bam.Net;

namespace Bam.Protocol.Server;

public class BamServerEventHandlers
{
    public BamServerEventHandlers()
    {
        this.StartingHandlers = new List<BamEventListener>();
        this.StartedHandlers = new List<BamEventListener>();
        this.StoppingHandlers = new List<BamEventListener>();
        this.StoppedHandlers = new List<BamEventListener>();
        this.TcpClientConnectedHandlers = new List<BamEventListener>();
        this.UdpDataReceivedHandlers = new List<BamEventListener>();
    }
    public List<BamEventListener> StartingHandlers { get; }
    public List<BamEventListener> StartedHandlers { get; }
    public List<BamEventListener> StoppingHandlers { get; }
    public List<BamEventListener> StoppedHandlers { get; }
    public List<BamEventListener> TcpClientConnectedHandlers { get; }
    public List<BamEventListener> UdpDataReceivedHandlers { get; }

    public bool HasHandlers =>
        StartedHandlers.Count > 0 || StartedHandlers.Count > 0 || StoppingHandlers.Count > 0 ||
        StoppedHandlers.Count > 0 || TcpClientConnectedHandlers.Count > 0 || UdpDataReceivedHandlers.Count > 0;

    internal void ListenTo(BamServer server)
    {
        List<BamEventListener> allEventHandlers = new List<BamEventListener>();
        allEventHandlers.AddRange(StartingHandlers);
        allEventHandlers.AddRange(StartedHandlers);
        allEventHandlers.AddRange(StoppingHandlers);
        allEventHandlers.AddRange(StoppedHandlers);
        allEventHandlers.AddRange(TcpClientConnectedHandlers);
        allEventHandlers.AddRange(UdpDataReceivedHandlers);

        foreach (BamEventListener bamEventListener in allEventHandlers)
        {
            server.On(bamEventListener.EventName, bamEventListener.EventHandler);
        }
    }
}