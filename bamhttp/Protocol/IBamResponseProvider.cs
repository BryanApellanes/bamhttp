namespace Bam.Protocol;

public interface IBamResponseProvider
{
    IBamResponse CreateResponse(IBamContext context);
}