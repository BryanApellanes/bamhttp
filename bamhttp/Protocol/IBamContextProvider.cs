namespace Bam.Protocol;

public interface IBamContextProvider
{
    IBamContext CreateContext(Stream stream);
}