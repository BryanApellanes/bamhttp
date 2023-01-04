namespace Bam.Protocol.Client
{
    public interface IBamClient
    {
         string BaseAddress { get; set; }
         IBamClientResponse SendRequest(IBamClientRequest request);
    }
}
