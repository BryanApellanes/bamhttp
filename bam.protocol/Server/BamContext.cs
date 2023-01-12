namespace Bam.Protocol.Server;

public class BamContext : IBamContext
{
    public NetworkProtocols RequestProtocol { get; set; }
    public string RequestId { get; internal set; }
    public IBamRequest BamRequest { get; internal set; }
    public IBamResponse BamResponse { get; set; }
    public IBamUser User { get; set; }
    public IBamSessionState SessionState { get; set; }
    public IBamAuthorizationCalculation AuthorizationCalculation { get; set; }
}