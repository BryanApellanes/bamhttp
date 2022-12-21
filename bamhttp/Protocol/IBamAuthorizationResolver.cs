namespace Bam.Protocol;

public interface IBamAuthorizationResolver
{
    BamAuthorizationResult ResolveAuthorization(IBamContext context);
}