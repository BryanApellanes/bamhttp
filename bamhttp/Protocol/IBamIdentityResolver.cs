namespace Bam.Protocol;

public interface IBamIdentityResolver
{
    IBamIdentity ResolveIdentity(IBamContext context);
}