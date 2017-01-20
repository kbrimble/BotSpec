using BotSpec.Assertions.Attachments;

namespace BotSpec.Assertions.Activities
{
    public interface ICanAssertActivityAttachments
    {
        IActivityAttachmentAssertions WithAttachment();
    }
}