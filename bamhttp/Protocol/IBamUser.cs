using System.Security.Principal;

namespace Bam.Protocol;

public interface IBamUser
{
    string UserName { get; set; }
    IPrincipal GetPrincipal();
}