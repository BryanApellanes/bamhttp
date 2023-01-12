using Bam.Net.Server;

namespace Bam.Protocol.Client
{
    public interface IBamClient
    {
         HostBinding TcpBaseAddress { get; set; }
         IBamClientRequestBuilder CreateRequestBuilder(BamClientProtocols protocol);
         IBamClientResponse ReceiveResponse(IBamClientRequest request);
    }
}
