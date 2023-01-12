using Bam.Net.Server;
using Bam.Protocol.Server;

namespace Bam.Protocol.Client;

public class HttpClientRequest : IBamClientRequest
{
    public HttpClientRequest()
    {
        Protocol = "HTTP";
        ProtocolVersion = "1.1";
        HttpMethod = HttpMethods.GET;
    }

    public HttpClientRequest(string content) : this()
    {
        Content = content;
    }

    public HttpClientRequest(BamClientRequestOptions options)
    {
        this.Host = options.Host;
    }

    public HostBinding Host { get; set; }
    public string Path { get; set; }
    public string QueryString { get; set; }
    public HttpMethods HttpMethod { get; set; }
    public string ProtocolVersion { get; set; }
    public string Protocol { get; set; }
    public object Content { get; set; }
    public Uri GetUrl(string baseAddress)
    {
        throw new NotImplementedException();
    }

    public BamRequestLine GetRequestLine()
    {
        throw new NotImplementedException();
    }
}