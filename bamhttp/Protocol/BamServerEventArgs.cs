using System.Net.Sockets;

namespace Bam.Protocol;

public class BamServerEventArgs : EventArgs
{
    public BamServerEventArgs()
    {
    }

    public BamServerEventArgs(TcpClient client)
    {
        this.LocalEndpoint = client?.Client?.LocalEndPoint?.ToString();
        this.RemoteEndpoint = client?.Client?.RemoteEndPoint?.ToString();
    }

    public BamHttpServer Server { get; set; }
    public string LocalEndpoint { get; set; }
    public string RemoteEndpoint { get; set; }
}