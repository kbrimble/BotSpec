using System.Collections.Generic;
using KBrimble.DirectLineTester.Assertions.Attachments;

namespace KBrimble.DirectLineTester.Assertions.Messages
{
    public interface IMessageAssertions : IHaveMessageAttachments
    {
        IMessageAssertions HaveTextMatching(string regex);
        IMessageAssertions HaveTextMatching(string regex, string groupMatchRegex, out IList<string> matchedGroups);
        IMessageAssertions BeFrom(string messageFrom);

    }

    public interface IHaveMessageAttachments
    {
        IMessageAttachmentAssertions HaveAttachment();
    }
}
