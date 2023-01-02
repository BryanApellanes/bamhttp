using Bam.Protocol.Server;

namespace Bam.Protocol;

public interface IBamRequestReader
{
    IBamRequest ReadRequest(Stream stream);
}