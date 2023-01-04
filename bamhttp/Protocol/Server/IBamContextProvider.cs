namespace Bam.Protocol.Server;

public interface IBamContextProvider
{
    IBamContext CreateContext(Stream stream, string requestId);
}