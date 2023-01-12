using Bam.Net.Server;
using Bam.Protocol.Server;

namespace Bam.Protocol.Client;

public class TcpClientRequest : IBamClientRequest
{
    public TcpClientRequest()
    {
        Protocol = "BAM";
        ProtocolVersion = "2.0";
        HttpMethod = HttpMethods.GET;
    }

    public TcpClientRequest(string content) : this()
    {
        Content = content;
    }

    public HostBinding Host { get; set; }
    public string Path { get; set; }
    public string QueryString { get; set; }
    public HttpMethods HttpMethod { get; set; }
    public string ProtocolVersion { get; set; }
    public string Protocol { get; set; }
    public object Content { get; set; }

    public Uri GetUrl(IBamClient client)
    {
        return GetUrl(client.TcpBaseAddress.ToString());
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