using System;

namespace KBrimble.DirectLineTester.Attachments
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
                    return AttachmentRetrieverSettings.CustomAttachmentRetriever;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}