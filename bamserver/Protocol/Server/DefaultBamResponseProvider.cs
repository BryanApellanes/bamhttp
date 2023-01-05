namespace Bam.Protocol.Server;

public class DefaultBamResponseProvider : BamResponseProvider
{
    public DefaultBamResponseProvider(IBamAuthorizationCalculator authorizationCalculator) : base(authorizationCalculator)
    {
    }

    public override IBamResponse CreateDeniedResponse(IBamContext context)
    {
        throw new NotImplementedException();
    }

    public override IBamResponse CreateReadResponse(IBamContext context)
    {
        throw new NotImplementedException();
    }

    public override IBamResponse CreateWriteResponse(IBamContext context)
    {
        throw new NotImplementedException();
    }

    public override void LogAccessDenied(IBamContext context)
    {
        throw new NotImplementedException();
    }
}