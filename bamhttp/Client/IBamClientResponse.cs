namespace Bam.Client;

public interface IBamClientResponse
{
    int StatusCode { get; }
    IBamClientResponse Authorize(IBamClientResponse clientResponse);
}