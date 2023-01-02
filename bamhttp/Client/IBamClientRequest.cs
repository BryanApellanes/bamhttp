namespace Bam.Client;

public interface IBamClientRequest
{
    string ProtocolVersion { get; set; }
    string Content { get; set; }
}