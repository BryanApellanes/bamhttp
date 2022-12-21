namespace Bam.Protocol;

public class BamRequestReaderOptions
{
    public BamRequestReaderOptions()
    {
        this.BufferSize = 5000;
    }
    
    public int BufferSize { get; set; }
    
    /// <summary>
    /// Gets or sets the event handler that is subscribed to all the events of a `RequestReader`.
    /// </summary>
    public EventHandler<BamRequestReaderEventArgs> EventHandler { get; set; }
}