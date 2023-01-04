namespace Bam.Protocol.Server;

public interface IBamIdentity : IBamUser
{
    string PhoneNumber { get; set; }
    string EmailAddress { get; set; }
    bool IsAuthenticated { get; set; }
}