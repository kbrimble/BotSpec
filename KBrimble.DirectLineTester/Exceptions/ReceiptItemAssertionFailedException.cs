namespace KBrimble.DirectLineTester.Exceptions
{
    public class ReceiptItemAssertionFailedException : BotAssertionFailedException
    {
        public ReceiptItemAssertionFailedException(string message) : base(message) {}
    }
}