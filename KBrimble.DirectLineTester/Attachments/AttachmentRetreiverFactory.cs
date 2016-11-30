namespace KBrimble.DirectLineTester.Attachments
{
    public static class AttachmentRetreiverFactory
    {
        public static IAttachmentRetreiver DefaultAttachmentRetreiver() => new AttachmentRetreiver();
    }
}