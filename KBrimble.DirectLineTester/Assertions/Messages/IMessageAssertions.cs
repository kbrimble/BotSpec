using System.Collections.Generic;

namespace KBrimble.DirectLineTester.Assertions.Messages
{
    public interface IMessageAssertions : ICanAssertMessageAttachments
    {
        IMessageAssertions HasTextMatching(string regex);
        IMessageAssertions HasTextMatching(string regex, string groupMatchRegex, out IList<string> matchedGroups);
        IMessageAssertions HasIdMatching(string regex);
        IMessageAssertions HasIdMatching(string regex, string groupMatchRegex, out IList<string> matchedGroups);
        IMessageAssertions HasFromMatching(string regex);
        IMessageAssertions HasFromMatching(string regex, string groupMatchRegex, out IList<string> matchedGroups);
    }
}
