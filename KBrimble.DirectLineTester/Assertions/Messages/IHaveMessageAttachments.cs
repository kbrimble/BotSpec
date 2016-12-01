using KBrimble.DirectLineTester.Assertions.Attachments;

namespace KBrimble.DirectLineTester.Assertions.Messages
{
    public interface IHaveMessageAttachments
    {
        IMessageAttachmentAssertions HaveAttachment();
    }
}