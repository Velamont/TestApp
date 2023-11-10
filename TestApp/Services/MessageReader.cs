using System.Text;

namespace TestApp;

public class MessageReader : IMessageReader
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
        using (var reader = new StreamReader(_stream, Encoding.UTF8, leaveOpen: true))
        {
            var stringBuilder = new StringBuilder();
            int currentByte;

            while ((currentByte = _stream.ReadByte()) != -1)
            {
                if (currentByte == _delimiter)
                {
                    break;
                }

                stringBuilder.Append((char)currentByte);
            }

            return stringBuilder.ToString();
        }
    }
}