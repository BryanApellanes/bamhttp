namespace Bam.Protocol.Server;

public interface IBamRequestProcessor
{
    void ProcessRequestContext(IBamContext context);
}