namespace Bam.Protocol.Server;

public interface IBamSessionStateProvider
{
    IBamSessionState GetSession(IBamContext context);
}