using System.Text;

namespace Bam.Protocol;

public static class ByteExtensions
{
    /// <summary>
    /// Gets the specified number of bytes from the end of the buffer.
    /// </summary>
    /// <param name="buffer">The buffer whose end the values are returned from.</param>
    /// <param name="count">The number of bytes to return.</param>
    /// <returns></returns>
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

    /// <summary>
    /// Gets a boolean indicating whether the end of the buffer equals the specified value.
    /// </summary>
    /// <param name="buffer">The buffer whose end is compared.</param>
    /// <param name="valueToCheck">The value to compare to the end of the buffer.</param>
    /// <param name="encoding">The encoding used to read the tail of the buffer.</param>
    /// <returns></returns>
    public static bool TailEquals(this byte[] buffer, string valueToCheck, Encoding encoding = null)
    {
        encoding = encoding ?? Encoding.ASCII;
        byte[] tail = buffer.PeekBack(valueToCheck.Length);
        string tailString = encoding.GetString(tail);
        return tailString.Equals(valueToCheck);
    }

    /// <summary>
    /// Removes trailing 0 (null) bytes from the buffer.
    /// </summary>
    /// <param name="buffer">The buffer to trim.</param>
    /// <returns>The a copy of the buffer with trailing 0 (null) bytes removed.</returns>
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