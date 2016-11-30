namespace KBrimble.DirectLineTester.Exceptions
{
    public class CardActionSetAssertionFailedException : BotAssertionFailedException
    {
        public CardActionSetAssertionFailedException(string message) : base(message) { }
    }
}