using System.Net.Sockets;
using Bam.Net;
using Bam.Protocol.Server;

namespace Bam.Protocol;

public class BamProtocolServerEventArgs : EventArgs
{
    public BamProtocolServerEventArgs()
    {
    }

    public BamProtocolServerEventArgs(IBamContext context)
    {
        this.Context = context;
    }
    
    public BamProtocolServerEventArgs(TcpClient client, IBamContext context = null)
    {
        this.LocalEndpoint = client?.Client?.LocalEndPoint?.ToString();
        this.RemoteEndpoint = client?.Client?.RemoteEndPoint?.ToString();
        this.Context = context;
    }

    public BamProtocolServer Server { get; set; }
    public IBamContext Context { get; internal set; }
    public string LocalEndpoint { get; private set; }
    public string RemoteEndpoint { get; private set; }
}