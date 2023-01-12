using System.Net;
using Bam.Net.Services;
using Bam.Protocol.Server;

namespace Bam.Protocol;

public class BamCommunicationHandler : IBamCommunicationHandler
{
    public ITcpIPAddressProvider TcpIPAddressProvider { get; internal set; }
    public IUdpIPAddressProvider UdpIPAddressProvider { get; internal set; }
    public IBamContextProvider ContextProvider { get; internal set; }
    public IBamResponseProvider ResponseProvider { get; internal set; }
    public IBamUserResolver UserResolver { get; internal set; }
    public IBamSessionStateProvider SessionStateProvider { get; internal set; }
    public IBamAuthorizationCalculator AuthorizationCalculator { get; internal set; }
    public IBamRequestProcessor RequestProcessor { get; internal set; }

    public virtual void Initialize(ApplicationServiceRegistry ComponentRegistry)
    {
        TcpIPAddressProvider = ComponentRegistry.Get<ITcpIPAddressProvider>(new BamTcpIPAddressProvider(IPAddress.Any));
        UdpIPAddressProvider = ComponentRegistry.Get<IUdpIPAddressProvider>(new BamUdpIPAddressProvider(IPAddress.Any));
        ResponseProvider = ComponentRegistry.Get<IBamResponseProvider>();
        ContextProvider = ComponentRegistry.Get<IBamContextProvider>();
        UserResolver = ComponentRegistry.Get<IBamUserResolver>();
        SessionStateProvider = ComponentRegistry.Get<IBamSessionStateProvider>();
        AuthorizationCalculator = ComponentRegistry.Get<IBamAuthorizationCalculator>();
        RequestProcessor = ComponentRegistry.Get<IBamRequestProcessor>();
    }
}