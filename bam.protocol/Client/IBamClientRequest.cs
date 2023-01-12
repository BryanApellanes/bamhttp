using Bam.Net.Server;
using Bam.Protocol.Server;

namespace Bam.Protocol.Client;

public interface IBamClientRequest
{
    HostBinding Host { get; set; }
    string Path { get; set; }
    string QueryString { get; set; }
    HttpMethods HttpMethod { get; set; }
    string ProtocolVersion { get; set; }
    string Protocol { get; set; }
    object Content { get; set; }

    Uri GetUrl(string baseAddress);

    BamRequestLine GetRequestLine();
}