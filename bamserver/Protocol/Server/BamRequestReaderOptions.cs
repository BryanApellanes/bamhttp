namespace Bam.Protocol.Server;

public class BamRequestReaderOptions
{
    public BamRequestReaderOptions(BamServerOptions serverOptions)
    {
        this.RequestBufferSize = serverOptions.RequestBufferSize;
    }
    
    public int RequestBufferSize { get; set; }
    
    /// <summary>
    /// Gets or sets the event handler that is subscribed to all the events of a `RequestReader`.
    /// </summary>
    public EventHandler<BamRequestReaderEventArgs> EventHandler { get; set; }
}