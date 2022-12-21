using System.Text;

namespace Bam.Protocol;

public static class ByteExtensions
{
    public static byte[] PeekBack(this byte[] buffer, int count)
    {
        if (buffer.Length < count)
        {
            return new byte[] { };
        }

        byte[] result = new byte[count];
        int index = 0;
        for (int i = buffer.Length - count; i < buffer.Length; i++)
        {
            result[index++] = buffer[i];
        }

        return result;
    }

    public static bool TailEquals(this byte[] buffer, string valueToCheck, Encoding encoding = null)
    {
        encoding = encoding ?? Encoding.ASCII;
        byte[] tail = buffer.PeekBack(valueToCheck.Length);
        string tailString = encoding.GetString(tail);
        return tailString.Equals(valueToCheck);
    }

    public static byte[] Trim(this byte[] buffer)
    {
        List<byte> returnBytes = new List<byte>();
        foreach (byte b in buffer)
        {
            if (b == 0)
            {
                break;
            }
            returnBytes.Add(b);
        }

        return returnBytes.ToArray();
    }
}