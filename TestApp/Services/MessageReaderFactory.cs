namespace TestApp;

public class MessageReaderFactory : IMessageReaderFactory
{
    public IMessageReader CreateMessageReader(Stream stream, byte delimiter)
    {
        return new MessageReader(stream, delimiter);
    }
}