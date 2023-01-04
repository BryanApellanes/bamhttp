using Bam.Net.Server;
using Bam.Protocol.Server;

namespace Bam.Protocol;

public class BamHostBinding : HostBinding
{
    public BamHostBinding()
    {
        Protocol = "bam";
    }

    public BamHostBinding(BamProtocolServerBuilder builder, HostBinding hostBinding)
    {
        Protocol = "bam";
        Port = builder.TcpPort();
        HostName = hostBinding.HostName;
    }
    
    public string Protocol { get; private set; }

    public override string ToString()
    {
        return $"{Protocol}://{HostName}:{Port}";
    }
}