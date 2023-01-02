/*
	Copyright © Bryan Apellanes 2022  
*/

using System.Security.Principal;

namespace Bam.Protocol.Server
{
    public interface IBamContext
    {
        NetworkProtocols RequestProtocol { get; set; }
        string RequestId { get; }
        IBamRequest BamRequest { get; }
        IBamResponse BamResponse { get; set; }
        IBamUser User { get; set; }
        IBamSessionState SessionState { get; set; }
        IBamAuthorizationCalculation AuthorizationCalculation { get; set; }
    }
}