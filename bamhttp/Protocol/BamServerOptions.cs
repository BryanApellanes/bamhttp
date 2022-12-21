using Bam.Net;
using Bam.Net.Logging;
using Bam.Net.Services;

namespace Bam.Protocol;

public class BamServerOptions
{
    public BamServerOptions()
    {
        this.Logger = Log.Default;
        this.Port = 8413;
        this.Name = 6.RandomLetters();
        this.RequestReader = new BamRequestReader();
        this.ComponentRegistry = ApplicationServiceRegistry.Current;
    }

    public BamServerOptions(ApplicationServiceRegistry componentRegistry): this()
    {
        this.ComponentRegistry = componentRegistry;
    }
    
    protected ApplicationServiceRegistry ComponentRegistry { get; private set; }
    
    public ILogger Logger { get; set; }
    public int Port { get; set; }
    public string Name { get; set; }

    public IBamRequestReader RequestReader
    {
        get => ComponentRegistry.Get<IBamRequestReader>();
        init => ComponentRegistry.Set<IBamRequestReader>(value);
    }

    public IBamSessionStateProvider SessionStateProvider
    {
        get => ComponentRegistry.Get<IBamSessionStateProvider>();
        set => ComponentRegistry.Set<IBamSessionStateProvider>(value);
    }
    
    public IBamContextProvider ContextProvider
    {
        get => ComponentRegistry.Get<IBamContextProvider>();
        init => ComponentRegistry.Set<IBamContextProvider>(value);
    }

    public IBamResponseProvider ResponseProvider
    {
        get => ComponentRegistry.Get<IBamResponseProvider>();
        init => ComponentRegistry.Set<IBamResponseProvider>(value);
    }

    public IBamIdentityResolver IdentityResolver
    {
        get => ComponentRegistry.Get<IBamIdentityResolver>();
        init => ComponentRegistry.Set<IBamIdentityResolver>(value);
    }

    public IBamUserResolver UserResolver
    {
        get => ComponentRegistry.Get<IBamUserResolver>();
        init => ComponentRegistry.Set<IBamUserResolver>(value);
    }

    public IBamAuthorizationResolver AuthorizationResolver
    {
        get => ComponentRegistry.Get<IBamAuthorizationResolver>();
        set => ComponentRegistry.Set<IBamAuthorizationResolver>(value);
    }
}