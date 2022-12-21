namespace Bam.Protocol;

public class BamStatusCodes
{
    static readonly Dictionary<int, string> _descriptions;
    static BamStatusCodes()
    {
        _descriptions = new Dictionary<int, string>
        {
            { 200, "OK" },
            { 404, "NOT FOUND" }
        };
    }
    
    public static string GetDescription(int code)
    {
        if (_descriptions.ContainsKey(code))
        {
            return _descriptions[code];
        }

        return string.Empty;
    }
}