namespace TestApp;

public interface IMessageReaderFactory
{
    IMessageReader CreateMessageReader(Stream stream, byte delimiter);
}