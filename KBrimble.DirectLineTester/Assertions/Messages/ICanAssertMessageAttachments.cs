using KBrimble.DirectLineTester.Assertions.Attachments;

namespace KBrimble.DirectLineTester.Assertions.Messages
{
    public interface ICanAssertMessageAttachments
    {
        IMessageAttachmentAssertions WithAttachment();
    }
}