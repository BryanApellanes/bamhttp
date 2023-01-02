using Bam.Protocol.Server;

namespace Bam.Protocol;

public interface IBamRequestProcessor
{
    void ProcessRequestContext(IBamContext context);
}