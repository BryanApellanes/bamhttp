using System.Net;

namespace Bam.Protocol.Server;

public interface IIPAddressProvider
{
    IPAddress GetIPAddress();
}