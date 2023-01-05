namespace Bam.Protocol.Client
{
    public interface IBamClient
    {
         string BaseAddress { get; set; }
         IBamClientResponse ReceiveResponse(IBamClientRequest request);
    }
}
