namespace Bam.Client
{
    public interface IBamClient
    {
         string BaseAddress { get; set; }
         IBamClientResponse Send(IBamClientRequest request);
    }
}
