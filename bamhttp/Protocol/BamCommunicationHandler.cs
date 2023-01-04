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
}