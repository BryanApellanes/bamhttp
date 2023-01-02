using Bam.Protocol.Server;

namespace Bam.Protocol;

public class BamContextEventArgs : EventArgs
{
    public BamContext Context { get; set; }
}