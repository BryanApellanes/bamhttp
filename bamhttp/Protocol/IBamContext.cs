/*
	Copyright © Bryan Apellanes 2022  
*/

using System.Security.Principal;

namespace Bam.Protocol
{
    public interface IBamContext
    {
        IBamRequest BamRequest { get; set; }
        IBamResponse BamResponse { get; set; }
        IBamUser User { get; set; }
        IBamSessionState SessionState { get; set; }
        
        IBamAuthorizationResult AuthorizationResult { get; set; }
    }
}