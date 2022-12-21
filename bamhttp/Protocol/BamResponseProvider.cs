using Bam.Net.Logging;
using CsQuery.EquationParser.Implementation.Functions;

namespace Bam.Protocol;

public abstract class BamResponseProvider : IBamResponseProvider
{
    public BamResponseProvider(IBamAuthorizationResolver authorizationResolver)
    {
        this.AuthorizationResolver = authorizationResolver;
    }
    
    private  IBamAuthorizationResolver AuthorizationResolver { get; set; } 
    
    public IBamResponse CreateResponse(IBamContext context)
    {
        BamAuthorizationResult authorizationResult = AuthorizationResolver.ResolveAuthorization(context);
        switch (authorizationResult.Access)
        {
            case BamAccess.Read:
                return CreateReadResponse(context);
                break;
            case BamAccess.Write:
                return CreateWriteResponse(context);
            default:
            case BamAccess.Denied:
                LogAccessDenied(context);
                return CreateDeniedResponse(context);
                break;
        }
    }

    public abstract IBamResponse CreateDeniedResponse(IBamContext context);
    public abstract IBamResponse CreateReadResponse(IBamContext context);
    public abstract IBamResponse CreateWriteResponse(IBamContext context);

    public abstract void LogAccessDenied(IBamContext context);
}