using System.Net;
using Bam.Net.Server;

namespace Bam.Protocol.Server;

public class BamServerInfo
{
    public string ServerName { get; set; }
    public int TcpPort { get; internal set; }
    public int UdpPort { get; internal set; }
    public string TcpIPAddress { get; internal set; }
    public string UdpIPAddress { get; internal set; }
    
    public HashSet<HostBinding> HostBindings { get; internal set; }
}