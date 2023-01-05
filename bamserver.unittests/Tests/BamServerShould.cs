using Bam.Net;
using Bam.Net.Testing.Unit;
using Bam.Protocol.Server;

namespace Bam.Protocol.Tests;

public class BamServerShould
{
    [UnitTest]
    public void FireServerStartAndStopEvents()
    {
        bool? startingEventRaised = false;
        bool? startedEventRaised = false;
        bool? stoppingEventRaised = false;
        bool? stoppedEventRaised = false;
        BamServer server = new BamServerBuilder()
            .OnStarting((sender, args) => startingEventRaised = true)
            .OnStarted((sender, args) => startedEventRaised = true)
            .OnStopping((sender, args) => stoppingEventRaised = true)
            .OnStopped((sender, args) => stoppedEventRaised = true)
            .Build();
        
        server.Start();
        server.Stop();
        
        startingEventRaised.ShouldBeTrue("`Starting` event was not raised");
        startedEventRaised.ShouldBeTrue("`Started` event was not raised");
        stoppingEventRaised.ShouldBeTrue("`Stopping` event was not raised");
        stoppedEventRaised.ShouldBeTrue("`Stopped` event was not raised");
    }

    [UnitTest]
    public void FireServerRequestEventsForTcpRequest()
    {
        
    }
}