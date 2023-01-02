using Bam.Net;
using Bam.Net.Logging;
using Bam.Net.Services;
using Bam.Protocol.Server;

namespace Bam.Protocol;

public class BamProtocolServerOptions
{
    public BamProtocolServerOptions()
    {
        this.Logger = Log.Default;
        this.TcpPort = BamProtocolServer.DefaultTcpPort;
        this.UdpPort = BamProtocolServer.DefaultUdpPort;
        this.Name = 6.RandomLetters();
        this.ComponentRegistry = ApplicationServiceRegistry.Current;
    }

    public BamProtocolServerOptions(ApplicationServiceRegistry componentRegistry): this()
    {
        this.ComponentRegistry = componentRegistry;
    }
    
    protected ApplicationServiceRegistry ComponentRegistry { get; private set; }
    
    public ILogger Logger { get; set; }
    public int TcpPort { get; set; }
    public int UdpPort { get; set; }
    public string Name { get; set; }

    private IBamCommunicationHandler _communicationHandler;
    public IBamCommunicationHandler GetCommunicationHandler(bool reinit = false)
    {
        if (_communicationHandler == null || reinit)
        {
            BamCommunicationHandler bamCommunicationHandler = new BamCommunicationHandler
            {
                ContextProvider = ComponentRegistry.Get<IBamContextProvider>(),
                ResponseProvider = ComponentRegistry.Get<IBamResponseProvider>(),
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