using Bam.Protocol.Server;

namespace Bam.Protocol;

public class BamAuthentication
{
    public bool Success { get; }
    public string[] Messages { get; }

    public IBamUser User { get; }
    
    public IBamRequest Request { get; }
}