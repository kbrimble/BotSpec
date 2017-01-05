namespace BotSpec.Exceptions
{
    public class ReceiptCardAssertionFailedException : BotAssertionFailedException
    {
        public ReceiptCardAssertionFailedException(string message) : base(message) {}
    }
}