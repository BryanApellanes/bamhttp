using System.Net;
using Bam.Net;
using Bam.Net.Incubation;
using Bam.Net.Logging;
using Bam.Net.Server;
using Bam.Net.Services;

namespace Bam.Protocol.Server;

public class BamServerOptions
{
    public BamServerOptions() : this(ApplicationServiceRegistry.ForProcess())
    {
    }

    public BamServerOptions(ApplicationServiceRegistry componentRegistry)
    {
        this.RequestBufferSize = 5000;
        this.Logger = Log.Default;
        this.TcpPort = BamServer.DefaultTcpPort;
        this.TcpIPAddress = IPAddress.Any;
        this.UdpPort = BamServer.DefaultUdpPort;
        this.UdpIPAddress = IPAddress.Any;
        this.ServerName = 6.RandomLetters();
        this.HostBindings = new List<HostBinding> { new HostBinding(TcpPort) };
        this.ComponentRegistry = componentRegistry;
        this.Initialize();
    }
    
    public ApplicationServiceRegistry ComponentRegistry { get; set; }
    public BamServerEventHandlers ServerEventHandlers { get; set; }
    public BamRequestEventHandlers RequestEventHandlers { get; set; }
    public List<HostBinding> HostBindings { get; set; }
    
    public int RequestBufferSize { get; set; }
    
    public ILogger Logger { get; set; }

    private int _tcpPort;

    public int TcpPort
    {
        get
        {
            if (_tcpPort <= 0 || UseNameBasedPort)
            {
                _tcpPort = BamPlatform.GetUnprivilegedPortForName(ServerName);
            }

            return _tcpPort;
        }
        set => _tcpPort = value;
    }
    public IPAddress TcpIPAddress { get; set; }

    private int _udpPort;
    public int UdpPort
    {
        get
        {
            if (_udpPort <= 0 || UseNameBasedPort)
            {
                _udpPort = TcpPort + 1;
            }

            return _udpPort;
        }
        set => _udpPort = value;
    }
    
    public IPAddress UdpIPAddress { get; set; }
    public string ServerName { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the Tcp and Udp ports should be deterministically derived from the name of the server.
    /// </summary>
    public bool UseNameBasedPort { get; set; }
    
    private void Initialize()
    {
        ServerEventHandlers = new BamServerEventHandlers();
        RequestEventHandlers = new BamRequestEventHandlers();
        ComponentRegistry
            .For<IBamRequestReader>().Use<BamRequestReader>()
            .For<IBamResponseProvider>().Use<DefaultBamResponseProvider>()
            .For<IBamContextProvider>().Use<BamContextProvider>()
            .For<IBamUserResolver>().Use<BamUserResolver>()
            .For<IBamSessionStateProvider>().Use<BamSessionStateProvider>()
            .For<IBamAuthorizationCalculator>().Use<BamAuthorizationCalculator>()
            .For<IBamRequestProcessor>().Use<BamRequestProcessor>();
    }

    internal void SubscribeEventHandlers(BamServer server)
    {
        if (ServerEventHandlers.HasHandlers)
        {
            SubscribeServerEventHandlers(server);
        }

        if (RequestEventHandlers.HasHandlers)
        {
            SubscribeRequestEventHandlers(server);
        }
    }

    internal void SubscribeServerEventHandlers(BamServer server)
    {
        ServerEventHandlers.ListenTo(server);
    }

    internal void SubscribeRequestEventHandlers(BamServer server)
    {
        RequestEventHandlers.ListenTo(server);
    }
    
    private IBamCommunicationHandler _communicationHandler;
    public virtual IBamCommunicationHandler GetCommunicationHandler(bool reinit = false)
    {
        if (_communicationHandler == null || reinit)
        {
            BamCommunicationHandler bamCommunicationHandler = new BamCommunicationHandler();
            bamCommunicationHandler.Initialize(ComponentRegistry);
            _communicationHandler = bamCommunicationHandler;
        }

        return _communicationHandler;
    }
}