using Expecto.Assertions.Attachments;

namespace Expecto.Assertions.Messages
{
    public interface ICanAssertMessageAttachments
    {
        IMessageAttachmentAssertions WithAttachment();
    }
}