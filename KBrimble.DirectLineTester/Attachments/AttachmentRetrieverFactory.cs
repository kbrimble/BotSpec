namespace KBrimble.DirectLineTester.Attachments
{
    public static class AttachmentRetrieverFactory
    {
        public static IAttachmentRetreiver DefaultAttachmentRetriever() => new AttachmentRetriever();
    }
}