namespace BotSpec.Exceptions
{
    public class ActivityAssertionFailedException : BotAssertionFailedException
    {
        public ActivityAssertionFailedException(string message) : base (message) { }
    }
}
