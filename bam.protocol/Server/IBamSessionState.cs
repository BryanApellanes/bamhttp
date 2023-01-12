namespace Bam.Protocol.Server;

public interface IBamSessionState
{
    object Get(string name);
    T Get<T>(string name);
    void Set(string name, object value);
    void Set<T>(string name, T value);
}