namespace BotSpec.Exceptions
{

    public class MessageAssertionFailedException : BotAssertionFailedException
    {
        public MessageAssertionFailedException(string message) : base (message) { }
    }
}
