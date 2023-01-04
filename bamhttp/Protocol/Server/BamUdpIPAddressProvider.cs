using System.Net;

namespace Bam.Protocol.Server;

public class BamUdpIPAddressProvider : BamIPAddressProvider, IUdpIPAddressProvider
{
    public BamUdpIPAddressProvider()
    {
        this._ipAddress = IPAddress.Any;
    }

    public BamUdpIPAddressProvider(string ipAddress) : this(IPAddress.Parse(ipAddress))
    {
    }

    public BamUdpIPAddressProvider(IPAddress ipAddress)
    {
        this._ipAddress = ipAddress;
    }

    private readonly IPAddress _ipAddress;
    public virtual IPAddress GetUdpIPAddress()
    {
        return _ipAddress;
    }
}