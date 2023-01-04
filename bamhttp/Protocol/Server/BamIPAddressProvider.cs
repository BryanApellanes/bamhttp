using System.Net;

namespace Bam.Protocol.Server;

public class BamIPAddressProvider: IIPAddressProvider
{
    public IPAddress GetIPAddress()
    {
        return IPAddress.Any;
    }
}