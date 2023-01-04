namespace Bam.Protocol.Server;

public interface IBamAuthorizationCalculation
{
    string[] Messages { get; }
    BamAccess Access { get; }
    IBamRequest Request { get; }
    IBamResponse Response { get; }
    IBamUser User { get; }
}