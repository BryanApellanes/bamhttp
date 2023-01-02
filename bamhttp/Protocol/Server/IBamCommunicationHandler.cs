namespace Bam.Protocol.Server;

public interface IBamCommunicationHandler
{
    IBamContextProvider ContextProvider { get; }
    IBamResponseProvider ResponseProvider { get; }
    IBamUserResolver UserResolver { get; }
    IBamSessionStateProvider SessionStateProvider { get; }
    IBamAuthorizationCalculator AuthorizationCalculator { get; }
    IBamRequestProcessor RequestProcessor { get; }
}