namespace Bam.Protocol.Server;

public interface IBamAuthenticator
{
    BamAuthentication Authenticate(IBamUser user);
}