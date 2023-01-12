using Bam.Net;
using Bam.Net.CommandLine;
using Bam.Net.Testing.Unit;
using Bam.Protocol.Client;
using Bam.Protocol.Server;

namespace Bam.Protocol.Tests;

public class BamClientShould
{
    [UnitTest]
    public void CreateHttpRequestBuilder()
    {
        BamClient client = new BamClient();
        IBamClientRequestBuilder requestBuilder = client.CreateRequestBuilder(BamClientProtocols.Http);
        
        requestBuilder.ShouldBeInstanceOfType<HttpBamClientRequestBuilder>();
    }

    [UnitTest]
    public void CreateTcpRequestBuilder()
    {
        BamClient client = new BamClient();
        IBamClientRequestBuilder requestBuilder = client.CreateRequestBuilder(BamClientProtocols.Tcp);
        
        requestBuilder.ShouldBeInstanceOfType<TcpBamClientRequestBuilder>();
    }
    
    [UnitTest]
    public void CreateUdpRequestBuilder()
    {
        BamClient client = new BamClient();
        IBamClientRequestBuilder requestBuilder = client.CreateRequestBuilder(BamClientProtocols.Udp);
        
        requestBuilder.ShouldBeInstanceOfType<UdpBamClientRequestBuilder>();
    }

    [UnitTest]
    public void CreateHttpRequestFromClient()
    {
        BamClient client = new BamClient();
        string httpPath = "/test/http/path/";
        IBamClientRequest request = client.CreateHttpRequest(httpPath);
        request.ShouldBeInstanceOfType<HttpClientRequest>();
        request.Host.ShouldBeEqualTo(BamClient.DefaultHttpBaseAddress);
        request.Path.ShouldBeEqualTo(httpPath);
        request.QueryString.ShouldBeNullOrEmpty();
        request.HttpMethod.ShouldBeEqualTo(HttpMethods.GET);
        request.Protocol.ShouldBeEqualTo("HTTP");
        request.ProtocolVersion.ShouldBeEqualTo("1.1");
        request.Content.ShouldBeNull();
    }
    
    [UnitTest]
    public void CreateTcpRequestFromClient()
    {
        BamClient client = new BamClient();
        string tcpPath = "/test/tcp/path";
        IBamClientRequest request = client.CreateTcpRequest(tcpPath);
        request.ShouldBeInstanceOfType<TcpClientRequest>();
        request.Host.ShouldBeEqualTo(BamClient.DefaultTcpBaseAddress);
        request.Path.ShouldBeEqualTo(tcpPath);
        request.QueryString.ShouldBeNullOrEmpty();
        request.HttpMethod.ShouldBeEqualTo(HttpMethods.POST);
        request.Protocol.ShouldBeEqualTo("BAM");
        request.ProtocolVersion.ShouldBeEqualTo("2.0");
        request.Content.ShouldBeNull();
    }
    
    [UnitTest]
    public void CreateUdpRequestFromClient()
    {
        BamClient client = new BamClient();
        string udpPath = "/test/udp/path";
        object content = "The content";
        IBamClientRequest request = client.CreateUdpRequest(udpPath, content);
        request.ShouldBeInstanceOfType<UdpClientRequest>();
        request.Host.ShouldBeEqualTo(BamClient.DefaultUdpBaseAddress);
        request.Path.ShouldBeEqualTo(udpPath);
        request.QueryString.ShouldBeNullOrEmpty();
        request.HttpMethod.ShouldBeEqualTo(HttpMethods.PUT);
        request.Protocol.ShouldBeEqualTo("BAM");
        request.ProtocolVersion.ShouldBeEqualTo("2.0");
        request.Content.ShouldBeEqualTo(content);
    }
    
    [UnitTest]
    public void ReceiveHttpResponse()
    {
        BamServer server = new BamServer();
        BamServerInfo info = server.GetInfo();
        Message.PrintLine(info.ToJson(true), ConsoleColor.Cyan);
        server.Start();

        BamClient client = new BamClient();
        
        
        
        server.Stop();
    }
}