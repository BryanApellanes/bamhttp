namespace Bam.Protocol;

public interface IBamAuthenticator
{
    BamAuthentication Authenticate(IBamUser user);
}