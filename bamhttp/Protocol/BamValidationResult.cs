namespace Bam.Protocol;

public class BamValidationResult
{
    public bool Success { get; internal set; }
    
    public string[] Messages { get; internal set; }
}