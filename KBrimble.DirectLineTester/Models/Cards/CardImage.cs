namespace KBrimble.DirectLineTester.Models.Cards
{
    /// <summary>
    /// An image on a card
    /// </summary>
    public class CardImage
    {
        /// <summary>
        /// URL Thumbnail image for major content property.
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// Image description intended for screen readers
        /// </summary>
        public string Alt { get; set; }

        /// <summary>
        /// Action assigned to specific Attachment.E.g.navigate to specific
        /// URL or play/open media content
        /// </summary>
        public CardAction Tap { get; set; }
    }
}
