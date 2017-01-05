namespace BotSpec.Attachments
{
    public static class AttachmentExtractorSettings
    {
        public static AttachmentExtractorType AttachmentExtractorType { get; set; } = AttachmentExtractorType.Default;

        public static IAttachmentExtractor CustomAttachmentExtractor { get; set; }
    }
}