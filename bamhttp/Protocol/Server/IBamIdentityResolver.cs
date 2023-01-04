namespace Bam.Protocol.Server;

public interface IBamIdentityResolver
{
    IBamIdentity ResolveIdentity(IBamContext context);
}