using System;

namespace KBrimble.DirectLineTester.Attachments
{
    public static class AttachmentRetrieverFactory
    {
        public static IAttachmentRetriever GetAttachmentRetriever()
        {
            switch (AttachmentExtractorSettings.AttachmentRetrieverType)
            {
                case AttachmentRetrieverType.Default:
                    return new DefaultAttachmentRetriever();
                case AttachmentRetrieverType.Custom:
                    return AttachmentExtractorSettings.CustomAttachmentRetriever;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}