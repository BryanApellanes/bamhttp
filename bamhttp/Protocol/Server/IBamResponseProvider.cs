namespace Bam.Protocol.Server;

public interface IBamResponseProvider
{
    IBamResponse CreateResponse(IBamContext context);
}