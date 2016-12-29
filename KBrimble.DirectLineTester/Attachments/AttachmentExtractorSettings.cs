namespace KBrimble.DirectLineTester.Attachments
{
    public static class AttachmentExtractorSettings
    {
        public static AttachmentRetrieverType AttachmentRetrieverType { get; set; } = AttachmentRetrieverType.Default;

        public static IAttachmentRetriever CustomAttachmentRetriever { get; set; }
    }
}
