namespace Bam.Protocol;

public class BamHeaderValue
{
    public static implicit operator string(BamHeaderValue header)
    {
        return header.Value;
    }
    
    public string Name { get; set; }
    public string Value { get; set; }
}