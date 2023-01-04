using System.Net;
using Bam.Net.Server;

namespace Bam.Protocol.Server;

public class BamProtocolServerInfo
{
    public string Name { get; set; }
    public int TcpPort { get; internal set; }
    public int UdpPort { get; internal set; }
    public IPAddress TcpIPAddress { get; internal set; }
    public IPAddress UdpIPAddress { get; internal set; }
    
    public HashSet<HostBinding> HostBindings { get; internal set; }
}