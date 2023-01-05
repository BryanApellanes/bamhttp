namespace Bam.Protocol.Server;

public interface IBamUserResolver
{
    IBamUser ResolveUser(IBamRequest request);
    
}