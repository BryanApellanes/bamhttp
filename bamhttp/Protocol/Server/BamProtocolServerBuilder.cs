using System.Net;
using Bam.Net.CoreServices;
using Bam.Net.Server;
using Bam.Net.Services;

namespace Bam.Protocol.Server;

public class BamProtocolServerBuilder
{
    private readonly BamServerEventHandlers _serverEventHandlers;
    private readonly BamRequestEventHandlers _requestEventHandlers;
    private readonly BamProtocolServerOptions _options;
    private ApplicationServiceRegistry _applicationServiceRegistry;
    public BamProtocolServerBuilder()
    {
        _serverEventHandlers = new BamServerEventHandlers();
        _requestEventHandlers = new BamRequestEventHandlers();
        _options = new BamProtocolServerOptions();
        _applicationServiceRegistry = ApplicationServiceRegistry.ForProcess();
    }

    public BamProtocolServerBuilder(ApplicationServiceRegistry applicationServiceRegistry)
    {
        _options = new BamProtocolServerOptions();
        _applicationServiceRegistry = applicationServiceRegistry;
    }

    internal int TcpPort()
    {
        return _options.TcpPort;
    }
    
    public BamProtocolServerBuilder TcpPort(int port)
    {
        _options.TcpPort = port;
        return this;
    }

    public BamProtocolServerBuilder UdpPort(int port)
    {
        _options.UdpPort = port;
        return this;
    }

    public BamProtocolServerBuilder TcpIPAddress(string ipAddress)
    {
        _options.TcpIPAddress = IPAddress.Parse(ipAddress);
        return this;
    }

    public BamProtocolServerBuilder UdpIPAddress(string ipAddress)
    {
        _options.UdpIPAddress = IPAddress.Parse(ipAddress);
        return this;
    }
    
    public BamProtocolServerBuilder TcpIPAddress(IPAddress ipAddress)
    {
        _options.TcpIPAddress = ipAddress;
        return this;
    }

    public BamProtocolServerBuilder UdpIPAddress(IPAddress ipAddress)
    {
        _options.UdpIPAddress = ipAddress;
        return this;
    }

    public BamProtocolServerBuilder Name(string name)
    {
        _options.Name = name;
        return this;
    }

    public BamProtocolServerBuilder HostBindings(params HostBinding[] hostBindings)
    {
        _options.HostBindings.AddRange(hostBindings.Select(hb => new BamHostBinding(this, hb)));
        return this;
    }
    
    public BamProtocolServerBuilder ComponentRegistry(ApplicationServiceRegistry componentRegistry)
    {
        _applicationServiceRegistry = componentRegistry;
        return this;
    }

    public BamProtocolServerBuilder Use<I, T>(T instance)
    {
        For<I>().Use(instance);
        return this;
    }

    public BamProtocolServerBuilder OnStarting(EventHandler<BamProtocolServerEventArgs> handler)
    {
        this._serverEventHandlers.StartingHandlers.Add(new BamEventListener(nameof(BamProtocolServer.Starting), handler));
        return this;
    }
    
    public BamProtocolServerBuilder OnStarted(EventHandler<BamProtocolServerEventArgs> handler)
    {
        this._serverEventHandlers.StartedHandlers.Add(new BamEventListener(nameof(BamProtocolServer.Started), handler));
        return this;
    }

    public BamProtocolServerBuilder OnTcpClientConnected(EventHandler<BamProtocolServerEventArgs> handler)
    {
        this._serverEventHandlers.TcpClientConnectedHandlers.Add(new BamEventListener(nameof(BamProtocolServer.TcpClientConnected), handler));
        return this;
    }

    public BamProtocolServerBuilder OnUdpDataReceived(EventHandler<BamProtocolServerEventArgs> handler)
    {
        this._serverEventHandlers.UdpDataReceivedHandlers.Add(new BamEventListener(nameof(BamProtocolServer.UdpDataReceived), handler));
        return this;
    }

    public BamProtocolServerBuilder OnStopping(EventHandler<BamProtocolServerEventArgs> handler)
    {
        this._serverEventHandlers.StoppingHandlers.Add(new BamEventListener(nameof(BamProtocolServer.Stopping), handler));
        return this;
    }
    
    public BamProtocolServerBuilder OnStopped(EventHandler<BamProtocolServerEventArgs> handler)
    {
        this._serverEventHandlers.StoppedHandlers.Add(new BamEventListener(nameof(BamProtocolServer.Stopped), handler));
        return this;
    }

    public BamProtocolServerBuilder OnCreateContextStarted(EventHandler<BamProtocolServerEventArgs> handler)
    {
        this._requestEventHandlers.CreateContextStartedHandlers.Add(new BamEventListener(nameof(BamProtocolServer.CreateContextStarted), handler));
        return this;
    }

    public BamProtocolServerBuilder OnCreateContextComplete(EventHandler<BamProtocolServerEventArgs> handler)
    {
        this._requestEventHandlers.CreateContextCompleteHandlers.Add(new BamEventListener(nameof(BamProtocolServer.CreateContextComplete), handler));
        return this;
    }
    
    public BamProtocolServerBuilder OnResolveUserStarted(EventHandler<BamProtocolServerEventArgs> handler)
    {
        this._requestEventHandlers.ResolveUserStartedHandlers.Add(new BamEventListener(nameof(BamProtocolServer.ResolveUserStarted), handler));
        return this;
    }
    
    public BamProtocolServerBuilder OnResolveUserComplete(EventHandler<BamProtocolServerEventArgs> handler)
    {
        this._requestEventHandlers.ResolveUserCompleteHandlers.Add(new BamEventListener(nameof(BamProtocolServer.ResolveUserComplete), handler));
        return this;
    }
    
    public BamProtocolServerBuilder OnAuthorizeRequestStarted(EventHandler<BamProtocolServerEventArgs> handler)
    {
        this._requestEventHandlers.AuthorizeRequestStartedHandlers.Add(new BamEventListener(nameof(BamProtocolServer.AuthorizeRequestStarted), handler));
        return this;
    }
    
    public BamProtocolServerBuilder OnAuthorizeRequestComplete(EventHandler<BamProtocolServerEventArgs> handler)
    {
        this._requestEventHandlers.AuthorizeRequestCompleteHandlers.Add(new BamEventListener(nameof(BamProtocolServer.AuthorizeRequestComplete), handler));
        return this;
    }
    
    public BamProtocolServerBuilder OnResolveSessionStateStarted(EventHandler<BamProtocolServerEventArgs> handler)
    {
        this._requestEventHandlers.ResolveSessionStateStartedHandlers.Add(new BamEventListener(nameof(BamProtocolServer.ResolveSessionStateStarted), handler));
        return this;
    }
    
    public BamProtocolServerBuilder OnResolveSessionStateComplete(EventHandler<BamProtocolServerEventArgs> handler)
    {
        this._requestEventHandlers.ResolveSessionStateCompleteHandlers.Add(new BamEventListener(nameof(BamProtocolServer.ResolveSessionStateComplete), handler));
        return this;
    }
    
    public BamProtocolServerBuilder OnCreateResponseStarted(EventHandler<BamProtocolServerEventArgs> handler)
    {
        this._requestEventHandlers.CreateResponseStartedHandlers.Add(new BamEventListener(nameof(BamProtocolServer.CreateResponseStarted), handler));
        return this;
    }
    
    public BamProtocolServerBuilder OnCreateResponseComplete(EventHandler<BamProtocolServerEventArgs> handler)
    {
        this._requestEventHandlers.CreateResponseCompleteHandlers.Add(new BamEventListener(nameof(BamProtocolServer.CreateResponseComplete), handler));
        return this;
    }
    
    public FluentServiceRegistryContext<I> For<I>()
    {
        return _applicationServiceRegistry.For<I>();
    }

    public BamProtocolServer Build()
    {
        _options.ComponentRegistry = _applicationServiceRegistry;
        _options.ServerEventHandlers = _serverEventHandlers;
        _options.RequestEventHandlers = _requestEventHandlers;
        return new BamProtocolServer(_options);
    }
}