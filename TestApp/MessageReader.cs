using System.Text;

namespace TestApp;

public class MessageReader
{
    private readonly Stream _stream;
    private readonly byte _delimiter;

    public MessageReader(Stream stream, byte delimiter)
    {
        _stream = stream ?? throw new ArgumentNullException(nameof(stream));
        _delimiter = delimiter;
    }

    public string ReadMessage()
    {
        using (var messageStream = new MemoryStream())
        {
            int currentByte;
            while ((currentByte = _stream.ReadByte()) != -1)
            {
                if (currentByte == _delimiter)
                {
                    break;
                }

                messageStream.WriteByte((byte)currentByte);
            }

            return Encoding.UTF8.GetString(messageStream.ToArray());
        }
    }
}