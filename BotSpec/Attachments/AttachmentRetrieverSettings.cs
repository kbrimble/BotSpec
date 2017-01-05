namespace BotSpec.Attachments
{
    public static class AttachmentRetrieverSettings
    {
        public static AttachmentRetrieverType AttachmentRetrieverType { get; set; } = AttachmentRetrieverType.Default;

        public static IAttachmentRetriever CustomAttachmentRetriever { get; set; }
    }
}
