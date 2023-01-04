using System.Net.Sockets;
using Bam.Net;

namespace Bam.Protocol.Server;

public class BamServerEventArgs : EventArgs
{
    public BamServerEventArgs()
    {
    }

    public BamServerEventArgs(IBamContext context)
    {
        this.Context = context;
    }
    
    public BamServerEventArgs(TcpClient client, IBamContext context = null)
    {
        this.LocalEndpoint = client?.Client?.LocalEndPoint?.ToString();
        this.RemoteEndpoint = client?.Client?.RemoteEndPoint?.ToString();
        this.Context = context;
    }

    public BamServer Server { get; set; }
    public IBamContext Context { get; internal set; }
    public string LocalEndpoint { get; private set; }
    public string RemoteEndpoint { get; private set; }
}