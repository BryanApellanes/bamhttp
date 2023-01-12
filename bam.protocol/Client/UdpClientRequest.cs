using Bam.Net.Server;
using Bam.Protocol.Server;

namespace Bam.Protocol.Client;

public class UdpClientRequest : IBamClientRequest
{
    public UdpClientRequest(string content)
    {
        Protocol = "BAM";
        ProtocolVersion = "2.0";
        HttpMethod = HttpMethods.PUT;
        Content = content;
    }

    public UdpClientRequest(object content)
    {
        Protocol = "BAM";
        ProtocolVersion = "2.0";
        HttpMethod = HttpMethods.PUT;
        Content = content;
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