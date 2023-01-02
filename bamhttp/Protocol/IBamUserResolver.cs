using Bam.Protocol.Server;

namespace Bam.Protocol;

public interface IBamUserResolver
{
    IBamUser ResolveUser(IBamRequest request);
    
}