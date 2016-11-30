namespace KBrimble.DirectLineTester.Exceptions
{
    public class CardActionAssertionFailedException : BotAssertionFailedException
    {
        public CardActionAssertionFailedException(string message) : base(message) { }
    }
}