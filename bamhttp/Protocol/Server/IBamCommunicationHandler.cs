namespace Bam.Protocol.Server;

public interface IBamCommunicationHandler
{
    ITcpIPAddressProvider TcpIPAddressProvider { get; }
    IUdpIPAddressProvider UdpIPAddressProvider { get; }
    IBamContextProvider ContextProvider { get; }
    IBamResponseProvider ResponseProvider { get; }
    IBamUserResolver UserResolver { get; }
    IBamSessionStateProvider SessionStateProvider { get; }
    IBamAuthorizationCalculator AuthorizationCalculator { get; }
    IBamRequestProcessor RequestProcessor { get; }
}