using System.Net;
using Bam.Net;
using Bam.Net.Incubation;
using Bam.Net.Logging;
using Bam.Net.Server;
using Bam.Net.Services;

namespace Bam.Protocol.Server;

public class BamServerOptions
{
    public BamServerOptions()
    {
        this.Logger = Log.Default;
        this.TcpPort = BamServer.DefaultTcpPort;
        this.TcpIPAddress = IPAddress.Any;
        this.UdpPort = BamServer.DefaultUdpPort;
        this.UdpIPAddress = IPAddress.Any;
        this.Name = 6.RandomLetters();
        this.HostBindings = new List<HostBinding>();
        this.ComponentRegistry = ApplicationServiceRegistry.ForProcess();
        this.Initialize();
    }

    public BamServerOptions(ApplicationServiceRegistry componentRegistry)
    {
        this.Logger = Log.Default;
        this.TcpPort = BamServer.DefaultTcpPort;
        this.TcpIPAddress = IPAddress.Any;
        this.UdpPort = BamServer.DefaultUdpPort;
        this.UdpIPAddress = IPAddress.Any;
        this.Name = 6.RandomLetters();
        this.HostBindings = new List<HostBinding>();
        this.ComponentRegistry = componentRegistry;
        this.Initialize();
    }
    
    public ApplicationServiceRegistry ComponentRegistry { get; set; }
    public BamServerEventHandlers ServerEventHandlers { get; set; }
    public BamRequestEventHandlers RequestEventHandlers { get; set; }
    public List<HostBinding> HostBindings { get; set; }
    
    public ILogger Logger { get; set; }
    public int TcpPort { get; set; }
    public IPAddress TcpIPAddress { get; set; }
    public int UdpPort { get; set; }
    public IPAddress UdpIPAddress { get; set; }
    public string Name { get; set; }

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
    public IBamCommunicationHandler GetCommunicationHandler(bool reinit = false)
    {
        if (_communicationHandler == null || reinit)
        {
            BamCommunicationHandler bamCommunicationHandler = new BamCommunicationHandler
            {
                TcpIPAddressProvider =  ComponentRegistry.Get<ITcpIPAddressProvider>(new BamTcpIPAddressProvider(TcpIPAddress)),
                UdpIPAddressProvider = ComponentRegistry.Get<IUdpIPAddressProvider>(new BamUdpIPAddressProvider(UdpIPAddress)),
                ResponseProvider = ComponentRegistry.Get<IBamResponseProvider>(),
                ContextProvider = ComponentRegistry.Get<IBamContextProvider>(),
                UserResolver = ComponentRegistry.Get<IBamUserResolver>(),
                SessionStateProvider = ComponentRegistry.Get<IBamSessionStateProvider>(),
                AuthorizationCalculator =  ComponentRegistry.Get<IBamAuthorizationCalculator>(),
                RequestProcessor = ComponentRegistry.Get<IBamRequestProcessor>()
            };
            _communicationHandler = bamCommunicationHandler;
        }

        return _communicationHandler;
    }
}