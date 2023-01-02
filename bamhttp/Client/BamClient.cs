namespace Bam.Client;

public class BamClient : IBamClient
{
    public BamClient()
    {
        this.HttpClient = new HttpClient();
    }

    public BamClient(HttpClient httpClient)
    {
        this.HttpClient = httpClient;
    }
    
    protected HttpClient HttpClient { get; set; }
    
    public string BaseAddress { get; set; }
    public IBamClientResponse Send(IBamClientRequest request)
    {
        throw new NotImplementedException();
    }
}