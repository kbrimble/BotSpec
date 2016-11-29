namespace KBrimble.DirectLineTester.Exceptions
{
    public class MessageSetAssertionFailedException : BotAssertionFailedException
    {
        public MessageSetAssertionFailedException(string message) : base(message) {}
    }
}
