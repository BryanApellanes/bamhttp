using System.Net;

namespace Bam.Protocol.Server;

public interface ITcpIPAddressProvider : IIPAddressProvider
{
    IPAddress GetTcpIPAddress();
}