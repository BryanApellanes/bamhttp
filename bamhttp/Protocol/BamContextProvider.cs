using Bam.Net.Logging;
using Bam.Client;
using Bam.Protocol.Server;

namespace Bam.Protocol;

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

    [Verbosity(VerbosityLevel.Information, MessageFormat = "EventName=ReadRequestStarted;LoggableIdentifier: {LoggableIdentifier}")]
    public event EventHandler ReadRequestStarted;
    
    [Verbosity(VerbosityLevel.Information, MessageFormat = "EventName=ResolveUserStarted;LoggableIdentifier: {LoggableIdentifier}")]
    public event EventHandler ResolveUserStarted;
    
    public IBamContext CreateContext(Stream stream)
    {
        IBamRequest request = RequestReader.ReadRequest(stream);
        return new BamContext
        {
            BamRequest = request,
            BamResponse = new BamResponse(stream)
        };
    }
}