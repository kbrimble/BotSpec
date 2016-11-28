using System;
namespace KBrimble.DirectLineTester
{
    public class NoMessageWithTextMatchingPatternException : Exception
    {
        const string MessageFormat = "No message matching pattern \"{0}\" was found";

        public NoMessageWithTextMatchingPatternException(string pattern) : base(GetExceptionMessage(pattern)) { }

        static string GetExceptionMessage(string messagePattern) => string.Format(MessageFormat, messagePattern);
    }
}
