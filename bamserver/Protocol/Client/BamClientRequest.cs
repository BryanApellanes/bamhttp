using Bam.Net.Server;
using Bam.Protocol.Server;

namespace Bam.Protocol.Client;

public class BamClientRequest : IBamClientRequest
{
    public BamClientRequest()
    {
        Protocol = "BAM";
        ProtocolVersion = "2.0";
        HttpMethod = HttpMethods.GET;
    }

    public BamClientRequest(string content) : this()
    {
        Content = content;
    }

    public HostBinding Host { get; set; }
    public string Path { get; set; }
    public string QueryString { get; set; }
    public HttpMethods HttpMethod { get; set; }
    public string ProtocolVersion { get; set; }
    public string Protocol { get; set; }
    public string Content { get; set; }

    public Uri GetUrl(IBamClient client)
    {
        return GetUrl((client.BaseAddress));
    }
    
    public Uri GetUrl(string baseAddress)
    {
        throw new NotImplementedException();
    }

    public BamRequestLine GetRequestLine()
    {
        throw new NotImplementedException();
    }
}