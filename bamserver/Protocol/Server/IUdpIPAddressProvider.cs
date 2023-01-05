using System.Net;

namespace Bam.Protocol.Server;

public interface IUdpIPAddressProvider : IIPAddressProvider
{
    IPAddress GetUdpIPAddress();
}