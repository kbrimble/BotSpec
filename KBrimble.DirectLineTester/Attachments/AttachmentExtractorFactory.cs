using System;

namespace KBrimble.DirectLineTester.Attachments
{
    public static class AttachmentExtractorFactory
    {
        public static IAttachmentExtractor GetAttachmentExtractor()
        {
            switch (AttachmentExtractorSettings.AttachmentExtractorType)
            {
                case AttachmentExtractorType.Default:
                    return new DefaultAttachmentExtractor();
                case AttachmentExtractorType.Custom:
                    return AttachmentExtractorSettings.CustomAttachmentExtractor;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}