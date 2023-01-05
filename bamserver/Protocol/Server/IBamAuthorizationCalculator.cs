namespace Bam.Protocol.Server;

public interface IBamAuthorizationCalculator
{
    BamAuthorizationCalculation CalculateAuthorization(IBamContext context);
}