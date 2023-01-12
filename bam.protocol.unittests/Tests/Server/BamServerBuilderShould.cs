using Bam.Net;
using Bam.Net.Server;
using Bam.Net.Testing.Unit;
using Bam.Protocol.Server;
using System.Net;

namespace Bam.Protocol.Tests;

public class BamServerBuilderShould
{    
    [UnitTest]
    public void BuildServer()
    {
        BamServer server = new BamServerBuilder().Build();
        server.ShouldNotBeNull();
    }
    
    [UnitTest]
    public void BuildBamProtocolServer()
    {
        int testTcpPort = RandomNumber.Between(1, 50);
        int testUdpPort = RandomNumber.Between(51, 100);
        string testHost1 = "testhost1";
        string testHost2 = "testhost2";
        string tcpIpAddress = "10.0.0.10";
        string udpIpAddress = "10.0.0.11";
        string serverName = "Test Server Name: ".RandomLetters(8);
        BamServer server = new BamServerBuilder()
            .TcpPort(testTcpPort)
            .UdpPort(testUdpPort)
            .ServerName(serverName)
            .TcpIPAddress(tcpIpAddress)
            .UdpIPAddress(udpIpAddress)
            .HostBindings(new HostBinding(testHost1), new HostBinding(testHost2))
            .Build();

        BamServerInfo info = server.GetInfo();
        info.ServerName.ShouldBeEqualTo(serverName);
        info.TcpPort.ShouldBeEqualTo(testTcpPort);
        info.UdpPort.ShouldBeEqualTo(testUdpPort);
        info.TcpIPAddress.ShouldBeEqualTo(IPAddress.Parse(tcpIpAddress).ToString());
        info.UdpIPAddress.ShouldBeEqualTo(IPAddress.Parse(udpIpAddress).ToString());
        List<int> hostBindingPorts = info.HostBindings.Select(hb => hb.Port).ToList();
        hostBindingPorts.Count.ShouldBeEqualTo(2);
        hostBindingPorts.Each(port => port.ShouldBeEqualTo(testTcpPort));
        List<string> hostBindings = info.HostBindings.Select(hb => hb.HostName).ToList();
        hostBindings.Contains(testHost1).ShouldBeTrue();
        hostBindings.Contains(testHost2).ShouldBeTrue();
    }
}