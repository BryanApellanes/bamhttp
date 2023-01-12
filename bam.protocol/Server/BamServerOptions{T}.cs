using Bam.Net.Services;

namespace Bam.Protocol.Server;

public class BamServerOptions<T> :BamServerOptions where T: BamCommunicationHandler, new()
{
    public BamServerOptions() : base(ApplicationServiceRegistry.ForProcess())
    {
    }

    public BamServerOptions(ApplicationServiceRegistry componentRegistry): base(componentRegistry)
    {
    }

    private IBamCommunicationHandler _communicationHandler;
    public virtual IBamCommunicationHandler GetCommunicationHandler(bool reinit = false)
    {
        if (_communicationHandler == null || reinit)
        {
            T bamCommunicationHandler = new T();
            bamCommunicationHandler.Initialize(ComponentRegistry);
            _communicationHandler = bamCommunicationHandler;
        }

        return _communicationHandler;
    }
}