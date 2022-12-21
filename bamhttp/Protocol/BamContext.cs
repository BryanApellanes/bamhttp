namespace Bam.Protocol;

public class BamContext : IBamContext
{
    public IBamRequest BamRequest { get; set; }
    public IBamResponse BamResponse { get; set; }
    public IBamUser User { get; set; }
    public IBamSessionState SessionState { get; set; }
    public IBamAuthorizationResult AuthorizationResult { get; set; }
}