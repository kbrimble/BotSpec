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
                    if (AttachmentExtractorSettings.CustomAttachmentExtractor == null)
                        throw new InvalidOperationException("Custom attachment extractor selected but one hasn't been set");
                    return AttachmentExtractorSettings.CustomAttachmentExtractor;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}