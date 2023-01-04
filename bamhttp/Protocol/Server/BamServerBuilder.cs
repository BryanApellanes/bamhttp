using System.Net;
using Bam.Net.CoreServices;
using Bam.Net.Server;
using Bam.Net.Services;

namespace Bam.Protocol.Server;

public class BamServerBuilder
{
    private readonly BamServerEventHandlers _serverEventHandlers;
    private readonly BamRequestEventHandlers _requestEventHandlers;
    private readonly BamServerOptions _options;
    private ApplicationServiceRegistry _applicationServiceRegistry;
    public BamServerBuilder()
    {
        _serverEventHandlers = new BamServerEventHandlers();
        _requestEventHandlers = new BamRequestEventHandlers();
        _options = new BamServerOptions();
        _applicationServiceRegistry = ApplicationServiceRegistry.ForProcess();
    }

    public BamServerBuilder(ApplicationServiceRegistry applicationServiceRegistry)
    {
        _options = new BamServerOptions();
        _applicationServiceRegistry = applicationServiceRegistry;
    }

    internal int TcpPort()
    {
        return _options.TcpPort;
    }
    
    public BamServerBuilder TcpPort(int port)
    {
        _options.TcpPort = port;
        return this;
    }

    public BamServerBuilder UdpPort(int port)
    {
        _options.UdpPort = port;
        return this;
    }

    public BamServerBuilder TcpIPAddress(string ipAddress)
    {
        _options.TcpIPAddress = IPAddress.Parse(ipAddress);
        return this;
    }

    public BamServerBuilder UdpIPAddress(string ipAddress)
    {
        _options.UdpIPAddress = IPAddress.Parse(ipAddress);
        return this;
    }
    
    public BamServerBuilder TcpIPAddress(IPAddress ipAddress)
    {
        _options.TcpIPAddress = ipAddress;
        return this;
    }

    public BamServerBuilder UdpIPAddress(IPAddress ipAddress)
    {
        _options.UdpIPAddress = ipAddress;
        return this;
    }

    public BamServerBuilder Name(string name)
    {
        _options.Name = name;
        return this;
    }

    public BamServerBuilder HostBindings(params HostBinding[] hostBindings)
    {
        _options.HostBindings.AddRange(hostBindings.Select(hb => new BamHostBinding(this, hb)));
        return this;
    }
    
    public BamServerBuilder ComponentRegistry(ApplicationServiceRegistry componentRegistry)
    {
        _applicationServiceRegistry = componentRegistry;
        return this;
    }

    public BamServerBuilder Use<I, T>(T instance)
    {
        For<I>().Use(instance);
        return this;
    }

    public BamServerBuilder OnStarting(EventHandler<BamServerEventArgs> handler)
    {
        this._serverEventHandlers.StartingHandlers.Add(new BamEventListener(nameof(BamServer.Starting), handler));
        return this;
    }
    
    public BamServerBuilder OnStarted(EventHandler<BamServerEventArgs> handler)
    {
        this._serverEventHandlers.StartedHandlers.Add(new BamEventListener(nameof(BamServer.Started), handler));
        return this;
    }

    public BamServerBuilder OnTcpClientConnected(EventHandler<BamServerEventArgs> handler)
    {
        this._serverEventHandlers.TcpClientConnectedHandlers.Add(new BamEventListener(nameof(BamServer.TcpClientConnected), handler));
        return this;
    }

    public BamServerBuilder OnUdpDataReceived(EventHandler<BamServerEventArgs> handler)
    {
        this._serverEventHandlers.UdpDataReceivedHandlers.Add(new BamEventListener(nameof(BamServer.UdpDataReceived), handler));
        return this;
    }

    public BamServerBuilder OnStopping(EventHandler<BamServerEventArgs> handler)
    {
        this._serverEventHandlers.StoppingHandlers.Add(new BamEventListener(nameof(BamServer.Stopping), handler));
        return this;
    }
    
    public BamServerBuilder OnStopped(EventHandler<BamServerEventArgs> handler)
    {
        this._serverEventHandlers.StoppedHandlers.Add(new BamEventListener(nameof(BamServer.Stopped), handler));
        return this;
    }

    public BamServerBuilder OnCreateContextStarted(EventHandler<BamServerEventArgs> handler)
    {
        this._requestEventHandlers.CreateContextStartedHandlers.Add(new BamEventListener(nameof(BamServer.CreateContextStarted), handler));
        return this;
    }

    public BamServerBuilder OnCreateContextComplete(EventHandler<BamServerEventArgs> handler)
    {
        this._requestEventHandlers.CreateContextCompleteHandlers.Add(new BamEventListener(nameof(BamServer.CreateContextComplete), handler));
        return this;
    }
    
    public BamServerBuilder OnResolveUserStarted(EventHandler<BamServerEventArgs> handler)
    {
        this._requestEventHandlers.ResolveUserStartedHandlers.Add(new BamEventListener(nameof(BamServer.ResolveUserStarted), handler));
        return this;
    }
    
    public BamServerBuilder OnResolveUserComplete(EventHandler<BamServerEventArgs> handler)
    {
        this._requestEventHandlers.ResolveUserCompleteHandlers.Add(new BamEventListener(nameof(BamServer.ResolveUserComplete), handler));
        return this;
    }
    
    public BamServerBuilder OnAuthorizeRequestStarted(EventHandler<BamServerEventArgs> handler)
    {
        this._requestEventHandlers.AuthorizeRequestStartedHandlers.Add(new BamEventListener(nameof(BamServer.AuthorizeRequestStarted), handler));
        return this;
    }
    
    public BamServerBuilder OnAuthorizeRequestComplete(EventHandler<BamServerEventArgs> handler)
    {
        this._requestEventHandlers.AuthorizeRequestCompleteHandlers.Add(new BamEventListener(nameof(BamServer.AuthorizeRequestComplete), handler));
        return this;
    }
    
    public BamServerBuilder OnResolveSessionStateStarted(EventHandler<BamServerEventArgs> handler)
    {
        this._requestEventHandlers.ResolveSessionStateStartedHandlers.Add(new BamEventListener(nameof(BamServer.ResolveSessionStateStarted), handler));
        return this;
    }
    
    public BamServerBuilder OnResolveSessionStateComplete(EventHandler<BamServerEventArgs> handler)
    {
        this._requestEventHandlers.ResolveSessionStateCompleteHandlers.Add(new BamEventListener(nameof(BamServer.ResolveSessionStateComplete), handler));
        return this;
    }
    
    public BamServerBuilder OnCreateResponseStarted(EventHandler<BamServerEventArgs> handler)
    {
        this._requestEventHandlers.CreateResponseStartedHandlers.Add(new BamEventListener(nameof(BamServer.CreateResponseStarted), handler));
        return this;
    }
    
    public BamServerBuilder OnCreateResponseComplete(EventHandler<BamServerEventArgs> handler)
    {
        this._requestEventHandlers.CreateResponseCompleteHandlers.Add(new BamEventListener(nameof(BamServer.CreateResponseComplete), handler));
        return this;
    }
    
    public FluentServiceRegistryContext<I> For<I>()
    {
        return _applicationServiceRegistry.For<I>();
    }

    public BamServer Build()
    {
        _options.ComponentRegistry = _applicationServiceRegistry;
        _options.ServerEventHandlers = _serverEventHandlers;
        _options.RequestEventHandlers = _requestEventHandlers;
        return new BamServer(_options);
    }
}