using Bam.Net.Logging;
using CsQuery.EquationParser.Implementation.Functions;

namespace Bam.Protocol.Server;

public abstract class BamResponseProvider : IBamResponseProvider
{
    public BamResponseProvider(IBamAuthorizationCalculator authorizationCalculator)
    {
        this.AuthorizationCalculator = authorizationCalculator;
    }
    
    private  IBamAuthorizationCalculator AuthorizationCalculator { get; set; } 
    
    public IBamResponse CreateResponse(IBamContext context)
    {
        BamAuthorizationCalculation authorizationCalculation = AuthorizationCalculator.CalculateAuthorization(context);
        switch (authorizationCalculation.Access)
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