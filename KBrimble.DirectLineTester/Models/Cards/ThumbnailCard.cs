using System.Collections.Generic;

namespace KBrimble.DirectLineTester.Models.Cards
{
    /// <summary>
    /// A thumbnail card (card with a single, small thumbnail image)
    /// </summary>
    public class ThumbnailCard : IHaveImages, IHaveButtons, IHaveTapAction
    {
        public const string ContentType = "application/vnd.microsoft.card.thumbnail";

        /// <summary>
        /// Initializes a new instance of the ThumbnailCard class.
        /// </summary>
        public ThumbnailCard(string title = default(string), string subtitle = default(string), string text = default(string), IList<CardImage> images = default(IList<CardImage>), IList<CardAction> buttons = default(IList<CardAction>), CardAction tap = default(CardAction))
        {
            Title = title;
            Subtitle = subtitle;
            Text = text;
            Images = images;
            Buttons = buttons;
            Tap = tap;
        }

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

        /// <inheritDoc/>
        public IList<CardAction> Buttons { get; set; }

        /// <inheritDoc/>
        public CardAction Tap { get; set; }

        /// <inheritDoc/>
        public IList<CardImage> Images { get; set; }
    }
}
