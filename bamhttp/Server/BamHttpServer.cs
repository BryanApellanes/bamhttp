/*
	Copyright © Bryan Apellanes 2015  
*/

using System.Net.Sockets;
using System.Text;
using System.Net;
using Bam.Net.Logging;
using Bam.Net.Configuration;
using Bam.Net;
using Bam.Net.Server.Streaming;
using DNS.Protocol.ResourceRecords;
using Google.Protobuf.WellKnownTypes;

namespace Bam.Protocol
{
    public class BamHttpServer: Loggable, IConfigurable, IDisposable
    {
        private bool _stopRequested;
        public BamHttpServer() : this(new BamServerOptions())
        {
        }

        public BamHttpServer(BamServerOptions options)
        {
            Logger = options.Logger;
            Port = options.Port;
            Name = options.Name;
            Options = options;
            Started += (o, a) => Subscribe(Logger);
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

        protected IBamContextProvider ContextProvider => Options.ContextProvider;
        protected IBamResponseProvider ResponseProvider => Options.ResponseProvider;
        protected IBamUserResolver UserResolver => Options.UserResolver;
        protected IBamIdentityResolver IdentityResolver => Options.IdentityResolver;
        protected IBamSessionStateProvider SessionStateProvider => Options.SessionStateProvider;
        protected IBamAuthorizationResolver AuthorizationResolver => Options.AuthorizationResolver;
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the size of the buffer to use when parsing requests.  This value
        /// determines the size of the read buffers when reading requests line by line.
        /// </summary>
        public int BufferSize
        {
            get;
            set;
        }
        
        public int Port
        {
            get;
            set;
        }

        public string[] RequiredProperties
        {
            get { return new string[] { "Port" }; }
        }

        public string LastExceptionMessage
        {
            get;
            set;
        }

        [Verbosity(VerbosityLevel.Information, SenderMessageFormat = "\r\nBamHttpServer={Name};Port={Port};Started")]
        public event EventHandler<BamServerEventArgs> Starting;

        [Verbosity(VerbosityLevel.Information, SenderMessageFormat = "\r\nBamHttpServer={Name};Port={Port};Started")]
        public event EventHandler<BamServerEventArgs> Started;


		[Verbosity(VerbosityLevel.Information, SenderMessageFormat = "\r\nBamHttpServer={Name};Port={Port};Stopped")]
        public event EventHandler<BamServerEventArgs> Stopped;

        [Verbosity(LogEventType.Error, SenderMessageFormat = "\r\nLastMessage: {LastExceptionMessage}")]
        public event EventHandler StartExceptionThrown;

        [Verbosity(LogEventType.Error, SenderMessageFormat = "\r\nLastMessage: {LastExceptionMessage}")]
        public event EventHandler RequestExceptionThrown;
        
        [Verbosity(LogEventType.Error, SenderMessageFormat = "\r\nLastMessage: {LastExceptionMessage}")]
        public event EventHandler InitializationException;

        [Verbosity(LogEventType.Information,
            SenderMessageFormat =
                "\r\nClient Connected: LocalEndpoint={LocalEndpoint}, RemoteEndpoint={RemoteEndpoint}")]
        public event EventHandler<BamServerEventArgs> ClientConnected;  

        public event EventHandler<BamServerEventArgs> CreateContextStarted;
        public event EventHandler<BamServerEventArgs> CreateContextComplete; 
        public event EventHandler<BamResponseProviderEventArgs> ResolveIdentityStarted;
        public event EventHandler<BamResponseProviderEventArgs> ResolvedIdentityComplete;

        public event EventHandler<BamServerEventArgs> ResolveUserStarted;
        public event EventHandler<BamServerEventArgs> ResolveUserComplete;
        
        public event EventHandler<BamServerEventArgs> ResolveSessionStateStarted;
        public event EventHandler<BamServerEventArgs> ResolveSessionStateComplete;
        public event EventHandler<BamServerEventArgs> AuthorizeRequestStarted;
        public event EventHandler<BamServerEventArgs> AuthorizeRequestComplete;

        public void Dispose()
        {
            Stop();
        }

        public void Stop()
        {
            _stopRequested = true;
            Listener.Stop();
            FireEvent(Stopped);
        }

        public void Start()
        {
            try
            {
                FireEvent(Starting);
                Listener = new TcpListener(IPAddress.Any, Port);

                try
                {
                    Listener.Start();

                    Task.Run(Listen);                    
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

        protected TcpListener Listener { get; set; }
        protected ILogger Logger { get; set; }

        protected virtual void Listen()
        {
            while (Listener != null && !_stopRequested)
            {
                TcpClient client = Listener.AcceptTcpClient();
                Task.Run(() =>
                {
                    try
                    {
                        FireEvent(ClientConnected, new BamServerEventArgs(client));
                        Logger.Info("Client Connected: LocalEndpoint={0}, RemoteEndpoint={1}", client.Client.LocalEndPoint.ToString(), client.Client.RemoteEndPoint.ToString());
                        ProcessRequest(client);
                    }
                    catch (Exception ex)
                    {
                        Logger.AddEntry("Error logging connection: {0}", ex, ex.Message);
                    }
                });
            }
        }

        protected internal virtual void ProcessRequest(TcpClient client)
        {
            int retryCount = 3;
            while (client.Connected)
            {
                try
                {
                    if (retryCount > 0)
                    {
                        NetworkStream stream = client.GetStream();
                        FireEvent(CreateContextStarted);
                        IBamContext context = ContextProvider.CreateContext(stream);
                        FireEvent(CreateContextComplete);
                        FireEvent(ResolveUserStarted);
                        context.User = UserResolver.ResolveUser(context.BamRequest);
                        FireEvent(ResolveUserComplete);
                        FireEvent(ResolveSessionStateStarted);
                        context.SessionState = SessionStateProvider.GetSession(context);
                        FireEvent(ResolveSessionStateComplete);
                        FireEvent(AuthorizeRequestStarted);
                        context.AuthorizationResult = AuthorizationResolver.ResolveAuthorization(context);
                        FireEvent(AuthorizeRequestComplete);
                        //FireEvent(CreateRes);
                        IBamResponse response = ResponseProvider.CreateResponse(context);
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