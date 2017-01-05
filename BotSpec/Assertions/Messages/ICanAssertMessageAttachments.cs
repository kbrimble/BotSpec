using BotSpec.Assertions.Attachments;

namespace BotSpec.Assertions.Messages
{
    public interface ICanAssertMessageAttachments
    {
        IMessageAttachmentAssertions WithAttachment();
    }
}