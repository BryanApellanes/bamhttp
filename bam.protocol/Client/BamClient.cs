using System.Net.Sockets;
using Bam.Net;
using Bam.Net.Web;
using Bam.Net.Server;
using Bam.Protocol.Server;

namespace Bam.Protocol.Client;

public class BamClient : IBamClient
{
    public static HostBinding DefaultTcpBaseAddress = new BamHostBinding("localhost", BamServer.DefaultTcpPort);

    public static HostBinding DefaultHttpBaseAddress = new HostBinding("localhost", BamServer.DefaultHttpPort);

    public static HostBinding DefaultUdpBaseAddress = new BamHostBinding("localhost", BamServer.DefaultUdpPort);
    
    public BamClient() : this(DefaultHttpBaseAddress, DefaultTcpBaseAddress, DefaultUdpBaseAddress)
    {
    }

    public BamClient(HostBinding httpBaseAddress) : this(httpBaseAddress, DefaultTcpBaseAddress, DefaultUdpBaseAddress)
    {
    }

    public BamClient(HostBinding httpBaseAddress, HostBinding tcpBaseAddress) : this(httpBaseAddress, tcpBaseAddress, DefaultUdpBaseAddress)
    {
    }
    
    public BamClient(HostBinding httpBaseAddress, HostBinding tcpBaseAddress, HostBinding udpBaseAddress)
    {
        this.HttpBaseAddress = httpBaseAddress;
        this.TcpBaseAddress = tcpBaseAddress;
        this.UdpBaseAddress = udpBaseAddress;
        this.Initialize();
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
        RequestBuilders = new Dictionary<BamClientProtocols, Func<IBamClientRequestBuilder>>()
        {
            { BamClientProtocols.Http, () => new HttpBamClientRequestBuilder() },
            { BamClientProtocols.Tcp, () => new TcpBamClientRequestBuilder() },
            { BamClientProtocols.Udp, () => new UdpBamClientRequestBuilder() }
        };
    }

    private HttpClient HttpClient // content
    {
        get; 
        set;
    }

    private TcpClient TcpClient // rpc
    {
        get;
        set;
    }
    
    private Socket UdpSocket // data and event broadcast
    {
        get;
        set;
    }
    
    private Dictionary<HttpMethods, HttpMethod> HttpMethods
    {
        get;
        set;
    }

    private Dictionary<BamClientProtocols, Func<IBamClientRequestBuilder>> RequestBuilders
    {
        get;
        set;
    }
    
    public HostBinding HttpBaseAddress { get; set; }
    public HostBinding TcpBaseAddress { get; set; }
    public HostBinding UdpBaseAddress { get; set; }

    public IBamClientRequest CreateHttpRequest(string path)
    {
        return CreateRequestBuilder(BamClientProtocols.Http)
            .BaseAddress(HttpBaseAddress)
            .Path(path)
            .Build();
    }

    public IBamClientRequest CreateTcpRequest(string path)
    {
        return CreateRequestBuilder(BamClientProtocols.Tcp)
            .BaseAddress(TcpBaseAddress)
            .Path(path)
            .Build();
    }

    public IBamClientRequest CreateUdpRequest(string path, object content)
    {
        return CreateRequestBuilder(BamClientProtocols.Udp)
            .BaseAddress(UdpBaseAddress)
            .Path(path)
            .Content(content)
            .Build();
    }
    
    public IBamClientRequestBuilder CreateRequestBuilder(BamClientProtocols protocol)
    {
        return RequestBuilders[protocol]();
    }

    public IBamClientResponse ReceiveResponse(IBamClientRequest request)
    {
        throw new NotImplementedException();
    }

    private HttpRequestMessage CreateHttpRequestMessage(IBamClientRequest request)
    {
        HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethods[request.HttpMethod], request.GetUrl(TcpBaseAddress.ToString()));
        requestMessage.Headers.Add(Headers.ProcessMode, ProcessMode.Current.Mode.ToString());
        requestMessage.Headers.Add(Headers.ProcessLocalIdentifier, Bam.Net.CoreServices.ApplicationRegistration.Data.ProcessDescriptor.LocalIdentifier);
        requestMessage.Headers.Add(Headers.ProcessDescriptor, Bam.Net.CoreServices.ApplicationRegistration.Data.ProcessDescriptor.Current.ToString());
        return requestMessage;
    }
}