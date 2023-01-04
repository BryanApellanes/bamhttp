using System.Net;

namespace Bam.Protocol.Server;

public class BamTcpIPAddressProvider : BamIPAddressProvider, ITcpIPAddressProvider
{
    public BamTcpIPAddressProvider()
    {
        this._ipAddress = IPAddress.Any;
    }

    public BamTcpIPAddressProvider(string ipAddress) : this(IPAddress.Parse(ipAddress))
    {
    }

    public BamTcpIPAddressProvider(IPAddress ipAddress)
    {
        this._ipAddress = ipAddress;
    }

    private readonly IPAddress _ipAddress;
    public virtual IPAddress GetTcpIPAddress()
    {
        return _ipAddress;
    }
}