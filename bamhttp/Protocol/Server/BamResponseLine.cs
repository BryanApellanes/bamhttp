namespace Bam.Protocol.Server;

public class BamResponseLine
{
    public BamResponseLine(BamRequest request): this(request.Line)
    {
    }

    protected BamResponseLine(BamRequestLine line)
    {
        this.ProtocolVersion = line.ProtocolVersion;
        this.StatusCode = 404;
    }
    
    public string ProtocolVersion { get; set; }
    public int StatusCode { get; set; }
    public string StatusDescription => BamStatusCodes.GetDescription(StatusCode);
    

    public override string ToString()
    {
        return $"{ProtocolVersion} {StatusCode} {StatusDescription}";
    }
}