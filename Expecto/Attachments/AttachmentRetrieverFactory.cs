using System;

namespace Expecto.Attachments
{
    public static class AttachmentRetrieverFactory
    {
        public static IAttachmentRetriever GetAttachmentRetriever()
        {
            switch (AttachmentRetrieverSettings.AttachmentRetrieverType)
            {
                case AttachmentRetrieverType.Default:
                    return new DefaultAttachmentRetriever();
                case AttachmentRetrieverType.Custom:
                    if (AttachmentRetrieverSettings.CustomAttachmentRetriever == null)
                        throw new InvalidOperationException("Custom attachment retriever selected but one hasn't been set");
                    return AttachmentRetrieverSettings.CustomAttachmentRetriever;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}