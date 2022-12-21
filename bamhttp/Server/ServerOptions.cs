using System.Net;
using System.Net.Sockets;
using Bam.Net.CommandLine;

namespace Bam.Protocol.Server;

public class ServerOptions
{
    [ConsoleAction]
    public void StartTcpServer()
    {
        TcpListener listener = new TcpListener(IPAddress.Any, 80);
        listener.Start();
        AutoResetEvent autoResetEvent = new AutoResetEvent(false);
        bool keepListening = true;
        while (keepListening)
        {
            
        }

        Console.ReadLine();
    }
}