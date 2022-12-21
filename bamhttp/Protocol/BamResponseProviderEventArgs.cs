namespace Bam.Protocol;

public class BamResponseProviderEventArgs : EventArgs
{
    public BamResponseProviderEventArgs()
    {
    }

    public BamResponseProviderEventArgs(BamResponseProvider provider)
    {
        this.ResponseProvider = provider;
    }
    
    public BamResponseProvider ResponseProvider { get; set; }
}