using System.Collections.Generic;

namespace KBrimble.DirectLineTester.Assertions
{
    public interface IMessageAssertions
    {
        IMessageAssertions HaveTextMatching(string regex);
        IMessageAssertions HaveTextMatching(string regex, string groupMatchRegex, out IEnumerable<string> matchedGroups);
        IMessageAssertions BeFrom(string messageFrom);
        IMessageAttachmentAssertions WithAttachment();
    }   
}
