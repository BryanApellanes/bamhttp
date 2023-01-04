using Bam.Net;
using Bam.Net.Web;
using Bam.Net.Server;
using Bam.Protocol.Server;

namespace Bam.Protocol.Client;

public class BamClient : IBamClient
{
    public static string DefaultHttpBaseAddress = $"bam://localhost:{BamProtocolServer.DefaultTcpPort}";
    public BamClient()
    {
        this.HttpClient = new HttpClient();
        this.Initialize();
    }

    public BamClient(HttpClient httpClient)
    {
        this.HttpClient = httpClient;
        this.Initialize();
    }

    public BamClient(string baseAddress) : this(new HttpClient() { BaseAddress = new Uri(baseAddress) })
    {
    }

    public BamClient(HostBinding hostBinding) : this(hostBinding.ToString())
    {
    }

    private void Initialize()
    {
        HttpMethods = new Dictionary<HttpMethods, HttpMethod>()
        {
            { Protocol.HttpMethods.GET, HttpMethod.Get },
            { Protocol.HttpMethods.POST, HttpMethod.Post },
            { Protocol.HttpMethods.PUT, HttpMethod.Put },
            { Protocol.HttpMethods.PATCH, HttpMethod.Patch },
            { Protocol.HttpMethods.DELETE, HttpMethod.Delete },
            { Protocol.HttpMethods.HEAD, HttpMethod.Head },
            { Protocol.HttpMethods.OPTIONS, HttpMethod.Options },
            { Protocol.HttpMethods.TRACE, HttpMethod.Trace },
        };
    }

    private HttpClient HttpClient
    {
        get; 
        set;
    }

    private Dictionary<HttpMethods, HttpMethod> HttpMethods
    {
        get;
        set;
    }

    public string BaseAddress { get; set; }
    public IBamClientResponse SendRequest(IBamClientRequest request)
    {
        throw new NotImplementedException();
    }

    private HttpRequestMessage CreateHttpRequestMessage(IBamClientRequest request)
    {
        HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethods[request.HttpMethod], request.GetUrl(BaseAddress));
        requestMessage.Headers.Add(Headers.ProcessMode, ProcessMode.Current.Mode.ToString());
        requestMessage.Headers.Add(Headers.ProcessLocalIdentifier, Bam.Net.CoreServices.ApplicationRegistration.Data.ProcessDescriptor.LocalIdentifier);
        requestMessage.Headers.Add(Headers.ProcessDescriptor, Bam.Net.CoreServices.ApplicationRegistration.Data.ProcessDescriptor.Current.ToString());
        return requestMessage;
    }
}