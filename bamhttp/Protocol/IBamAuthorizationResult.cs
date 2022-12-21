namespace Bam.Protocol;

public interface IBamAuthorizationResult
{
    string[] Messages { get; }
    BamAccess Access { get; }
    IBamRequest Request { get; }
    IBamResponse Response { get; }
    IBamUser User { get; }
}