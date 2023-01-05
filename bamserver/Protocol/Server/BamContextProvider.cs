using Bam.Net.Logging;

namespace Bam.Protocol.Server;

public class BamContextProvider : Loggable, IBamContextProvider
{
    public BamContextProvider(IBamRequestReader requestReader, IBamResponseProvider responseProvider, IBamUserResolver userResolver)
    {
        this.RequestReader = requestReader;
        this.ResponseProvider = responseProvider;
        this.UserResolver = userResolver;
    }
    protected IBamRequestReader RequestReader { get; private set; }
    protected  IBamResponseProvider ResponseProvider { get; private set; }
    protected  IBamUserResolver UserResolver { get; private set; }

    [Verbosity(VerbosityLevel.Information, MessageFormat = $"EventName={nameof(ReadRequestStarted)};LoggableIdentifier: {{LoggableIdentifier}}")]
    public event EventHandler ReadRequestStarted;
    
    [Verbosity(VerbosityLevel.Information, MessageFormat = $"EventName={nameof(ResolveUserStarted)};LoggableIdentifier: {{LoggableIdentifier}}")]
    public event EventHandler ResolveUserStarted;
    
    public IBamContext CreateContext(Stream stream, string requestId)
    {
        IBamRequest request = RequestReader.ReadRequest(stream);
        return new BamContext
        {
            RequestId = requestId,
            BamRequest = request,
            BamResponse = new BamResponse(stream)
        };
    }
}