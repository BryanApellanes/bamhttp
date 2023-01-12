using System.Security.Principal;

namespace Bam.Protocol.Server;

public interface IBamUser
{
    string UserName { get; set; }
    IPrincipal GetPrincipal();
}