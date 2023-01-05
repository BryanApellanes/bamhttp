/*
	Copyright © Bryan Apellanes 2015  
*/

using System.Net.Sockets;
using System.Text;
using System.Net;
using Bam.Net.Logging;
using Bam.Net.Configuration;
using Bam.Net;
using Bam.Net.CoreServices.ApplicationRegistration.Data;
using Bam.Net.Server;
using Bam.Net.Server.Streaming;
using DNS.Protocol.ResourceRecords;
using Google.Protobuf.WellKnownTypes;

namespace Bam.Protocol.Server
{
    public class BamServer: Loggable, IConfigurable, IDisposable
    {
        private bool _stopRequested;
        public const int DefaultTcpPort = 8413;
        public const int DefaultUdpPort = 8414;
        public BamServer() : this(new BamServerOptions())
        {
        }

        public BamServer(BamServerOptions options)
        {
            Logger = options.Logger;
            TcpPort = options.TcpPort;
            UdpPort = options.UdpPort;
            ServerName = options.ServerName;
            HostBindings = new HashSet<HostBinding>(options.HostBindings.Select(hb=> new HostBinding(hb.HostName, TcpPort)));
            Options = options;
            Started += (o, a) => Subscribe(Logger);
            Options.SubscribeEventHandlers(this);
        }
        
        Encoding _encoding;
        readonly object _encodingLock = new object();

        protected Encoding Encoding
        {
            get
            {
                return _encodingLock.DoubleCheckLock(ref _encoding, () => Encoding.UTF8);
            }
            set => _encoding = value;
        }
        
        protected BamServerOptions Options { get; private set; }

        protected IBamCommunicationHandler CommunicationHandler => Options.GetCommunicationHandler();

        protected ITcpIPAddressProvider TcpIPAddressProvider => CommunicationHandler.TcpIPAddressProvider;
        protected IUdpIPAddressProvider UdpIPAddressProvider => CommunicationHandler.UdpIPAddressProvider;
        protected IBamContextProvider ContextProvider => CommunicationHandler.ContextProvider;
        protected IBamResponseProvider ResponseProvider => CommunicationHandler.ResponseProvider;
        protected IBamUserResolver UserResolver => CommunicationHandler.UserResolver;
        protected IBamSessionStateProvider SessionStateProvider => CommunicationHandler.SessionStateProvider;
        protected IBamAuthorizationCalculator AuthorizationCalculator => CommunicationHandler.AuthorizationCalculator;
        protected IBamRequestProcessor RequestProcessor => CommunicationHandler.RequestProcessor;
        
        /// <summary>
        /// Gets or sets the name of this server to aide in identifying the process in logs.
        /// </summary>
        public string ServerName { get; set; }

        public HashSet<HostBinding> HostBindings { get; }
        
        public int TcpPort
        {
            get;
            set;
        }

        public IPAddress TcpIPAddress
        {
            get => TcpIPAddressProvider.GetTcpIPAddress();
        }

        public IPAddress UdpIPAddress
        {
            get => UdpIPAddressProvider.GetUdpIPAddress();
        }
        
        public int UdpPort
        {
            get;
            set;
        }

        public string[] RequiredProperties
        {
            get { return new string[] { nameof(TcpPort) }; }
        }

        public string LastExceptionMessage
        {
            get;
            set;
        }

        [Verbosity(VerbosityLevel.Information, SenderMessageFormat = "BamHttpServer={Name};Port={Port};Started")]
        public event EventHandler<BamServerEventArgs> Starting;

        [Verbosity(VerbosityLevel.Information, SenderMessageFormat = "BamHttpServer={Name};Port={Port};Started")]
        public event EventHandler<BamServerEventArgs> Started;

        [Verbosity(VerbosityLevel.Information, SenderMessageFormat = "BamHttpServer={Name};Port={Port};Stopping")]
        public event EventHandler<BamServerEventArgs> Stopping;        
        
		[Verbosity(VerbosityLevel.Information, SenderMessageFormat = "BamHttpServer={Name};Port={Port};Stopped")]
        public event EventHandler<BamServerEventArgs> Stopped;

        [Verbosity(LogEventType.Error, SenderMessageFormat = "LastMessage: {LastExceptionMessage}")]
        public event EventHandler StartExceptionThrown;

        [Verbosity(LogEventType.Error, SenderMessageFormat = "LastMessage: {LastExceptionMessage}")]
        public event EventHandler RequestExceptionThrown;
        
        [Verbosity(LogEventType.Error, SenderMessageFormat = "LastMessage: {LastExceptionMessage}")]
        public event EventHandler InitializationException;

        [Verbosity(LogEventType.Information,
            SenderMessageFormat =
                "Client Connected: LocalEndpoint={LocalEndpoint}, RemoteEndpoint={RemoteEndpoint}")]
        public event EventHandler<BamServerEventArgs> TcpClientConnected;

        public event EventHandler<BamServerEventArgs> UdpDataReceived; 

        public event EventHandler<BamServerEventArgs> CreateContextStarted;
        public event EventHandler<BamServerEventArgs> CreateContextComplete; 
        public event EventHandler<BamServerEventArgs> ResolveUserStarted;
        public event EventHandler<BamServerEventArgs> ResolveUserComplete;
        public event EventHandler<BamServerEventArgs> AuthorizeRequestStarted;
        public event EventHandler<BamServerEventArgs> AuthorizeRequestComplete;
        public event EventHandler<BamServerEventArgs> ResolveSessionStateStarted;
        public event EventHandler<BamServerEventArgs> ResolveSessionStateComplete;

        public event EventHandler<BamServerEventArgs> CreateResponseStarted;
        public event EventHandler<BamServerEventArgs> CreateResponseComplete;

        public BamServerInfo GetInfo()
        {
            BamServerInfo info = new BamServerInfo();
            info.CopyProperties(this);
            info.TcpIPAddress = TcpIPAddress.Equals(IPAddress.Any) ? "Any" : TcpIPAddress.ToString();
            info.UdpIPAddress = UdpIPAddress.Equals(IPAddress.Any) ? "Any" : UdpIPAddress.ToString();
            return info;
        }
        
        public void Dispose()
        {
            Stop();
        }

        public void Stop()
        {
            FireEvent(Stopping);
            _stopRequested = true;
            TcpListener.Stop();
            FireEvent(Stopped);
        }

        public void Start()
        {
            try
            {
                FireEvent(Starting);
                try
                {
                    TcpListener = new TcpListener(TcpIPAddress, TcpPort);
                    TcpListener.Start();
                    Task.Run(HandleTcpRequests);
                    Task.Run(HandleUdpRequests);
                }
                catch (Exception ex)
                {
                    LastExceptionMessage = ex.Message;
                    FireEvent(StartExceptionThrown, new ErrorEventArgs(ex));
                }
                FireEvent(Started);
            }
            catch (Exception ex)
            {
                LastExceptionMessage = ex.Message;
                FireEvent(InitializationException, new ErrorEventArgs(ex));
            }
        }

        protected TcpListener TcpListener { get; set; }
        protected UdpClient UdpClient { get; set; }
        protected ILogger Logger { get; set; }

        protected void HandleUdpRequests()
        {
            UdpClient = new UdpClient(UdpPort);
            IPEndPoint groupEndpoint = new IPEndPoint(UdpIPAddress, UdpPort);
            while (UdpClient != null && !_stopRequested)
            {
                byte[] data = UdpClient.Receive(ref groupEndpoint);
                Task.Run(() =>
                {
                    string requestId = Cuid.Generate();
                    MemoryStream stream = new MemoryStream(data);
                    FireEvent(CreateContextStarted, new BamServerEventArgs());
                        
                    IBamContext context = ContextProvider.CreateContext(stream, requestId);
                    context.RequestProtocol = NetworkProtocols.Udp;
                    FireEvent(ResolveUserStarted, new BamServerEventArgs(context));
                    context.User = UserResolver.ResolveUser(context.BamRequest);
                    FireEvent(ResolveUserComplete, new BamServerEventArgs(context));
                    FireEvent(AuthorizeRequestStarted, new BamServerEventArgs(context));
                    context.AuthorizationCalculation = AuthorizationCalculator.CalculateAuthorization(context);
                    FireEvent(AuthorizeRequestComplete, new BamServerEventArgs(context));
                    FireEvent(ResolveSessionStateStarted, new BamServerEventArgs(context));
                    context.SessionState = SessionStateProvider.GetSession(context);
                    FireEvent(ResolveSessionStateComplete, new BamServerEventArgs(context));
                    FireEvent(CreateResponseStarted, new BamServerEventArgs(context));
                    context.BamResponse = ResponseProvider.CreateResponse(context);
                    FireEvent(CreateResponseComplete, new BamServerEventArgs(context));
                        
                    FireEvent(CreateContextComplete, new BamServerEventArgs(context));
                });
            }
        }
        
        protected virtual void HandleTcpRequests()
        {
            while (TcpListener != null && !_stopRequested)
            {
                TcpClient client = TcpListener.AcceptTcpClient();
                Task.Run(() =>
                {
                    string requestId = Cuid.Generate();
                    try
                    {
                        FireEvent(TcpClientConnected, new BamServerEventArgs(client));
                        Logger.Info("Tcp Client Connected (CorrelationId={0}): LocalEndpoint={1}, RemoteEndpoint={2}", requestId, client.Client.LocalEndPoint.ToString(), client.Client.RemoteEndPoint.ToString());
                        HandleTcpRequest(client, requestId);
                    }
                    catch (Exception ex)
                    {
                        Logger.AddEntry("Error parsing tcp request (CorrelationId={0}): {1}", ex, requestId, ex.Message);
                    }
                });
            }
        }

        protected internal virtual void HandleTcpRequest(TcpClient client, string requestId)
        {
            int retryCount = 3;
            while (client.Connected)
            {
                try
                {
                    if (retryCount > 0)
                    {
                        NetworkStream stream = client.GetStream();
                        FireEvent(CreateContextStarted, new BamServerEventArgs(client));
                        
                        IBamContext context = ContextProvider.CreateContext(stream, requestId);
                        context.RequestProtocol = NetworkProtocols.Tcp;
                        FireEvent(ResolveUserStarted, new BamServerEventArgs(client, context));
                        context.User = UserResolver.ResolveUser(context.BamRequest);
                        FireEvent(ResolveUserComplete, new BamServerEventArgs(client, context));
                        FireEvent(AuthorizeRequestStarted, new BamServerEventArgs(client, context));
                        context.AuthorizationCalculation = AuthorizationCalculator.CalculateAuthorization(context);
                        FireEvent(AuthorizeRequestComplete, new BamServerEventArgs(client, context));
                        FireEvent(ResolveSessionStateStarted, new BamServerEventArgs(client, context));
                        context.SessionState = SessionStateProvider.GetSession(context);
                        FireEvent(ResolveSessionStateComplete, new BamServerEventArgs(client, context));
                        FireEvent(CreateResponseStarted, new BamServerEventArgs(client, context));
                        context.BamResponse = ResponseProvider.CreateResponse(context);
                        FireEvent(CreateResponseComplete, new BamServerEventArgs(client, context));
                        FireEvent(CreateContextComplete, new BamServerEventArgs(client, context));
                    }
                }
                catch (Exception ex)
                {
                    FireEvent(RequestExceptionThrown, new ErrorEventArgs(ex));
                    Logger.AddEntry("Error processing request (retryCount={0}): {1}", ex, retryCount.ToString(), ex.Message);
                    Thread.Sleep(30);
                    --retryCount;
                }
            }            
        }

        public void Configure(IConfigurer configurer)
        {
            configurer.Configure(this);
            this.CheckRequiredProperties();
        }

        public void Configure(object configuration)
        {
            this.CopyProperties(configuration);
            this.CheckRequiredProperties();
        }
    }
}