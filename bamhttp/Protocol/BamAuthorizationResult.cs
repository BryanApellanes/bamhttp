namespace Bam.Protocol;

public class BamAuthorizationResult : IBamAuthorizationResult
{
    public BamAuthorizationResult(IBamContext context, BamAccess access)
    {
        this.Context = context;
        this.Access = access;
    }
    
    private IBamContext Context { get; set; }
    
    public string[] Messages { get; internal set; }
    public BamAccess Access { get; internal set; }
    public IBamRequest Request => Context.BamRequest;
    public IBamResponse Response => Context.BamResponse;
    public IBamUser User => Context.User;
}