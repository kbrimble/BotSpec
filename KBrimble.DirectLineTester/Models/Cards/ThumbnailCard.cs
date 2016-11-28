using System.Collections.Generic;

namespace KBrimble.DirectLineTester
{
    /// <summary>
    /// A thumbnail card (card with a single, small thumbnail image)
    /// </summary>
    public class ThumbnailCard
    {
        public const string ContentType = "application/vnd.microsoft.card.thumbnail";

        /// <summary>
        /// Title of the card
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Subtitle of the card
        /// </summary>
        public string Subtitle { get; set; }

        /// <summary>
        /// Text for the card
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Array of images for the card
        /// </summary>
        public IList<CardImage> Images { get; set; }

        /// <summary>
        /// Set of actions applicable to the current card
        /// </summary>
        public IList<CardAction> Buttons { get; set; }

        /// <summary>
        /// This action will be activated when user taps on the card itself
        /// </summary>
        public CardAction Tap { get; set; }
    }
}
