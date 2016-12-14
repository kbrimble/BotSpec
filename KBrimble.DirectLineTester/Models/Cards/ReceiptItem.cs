namespace KBrimble.DirectLineTester.Models.Cards
{
    public class ReceiptItem : IHaveAnImage, IHaveTapAction
    {
        /// <summary>
        /// Initializes a new instance of the ReceiptItem class.
        /// </summary>
        public ReceiptItem() { }

        /// <summary>
        /// Initializes a new instance of the ReceiptItem class.
        /// </summary>
        public ReceiptItem(string title = default(string), string subtitle = default(string), string text = default(string), CardImage image = default(CardImage), string price = default(string), string quantity = default(string), CardAction tap = default(CardAction))
        {
            Title = title;
            Subtitle = subtitle;
            Text = text;
            Image = image;
            Price = price;
            Quantity = quantity;
            Tap = tap;
        }

        /// <summary>
        /// Title of the Card
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Subtitle appears just below Title field, differs from Title in
        /// font styling only
        /// </summary>
        public string Subtitle { get; set; }

        /// <summary>
        /// Text field appears just below subtitle, differs from Subtitle in
        /// font styling only
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Amount with currency
        /// </summary>
        public string Price { get; set; }

        /// <summary>
        /// Number of items of given kind
        /// </summary>
        public string Quantity { get; set; }

        /// <inheritdoc/>
        public CardImage Image { get; set; }

        /// <inheritdoc/>
        public CardAction Tap { get; set; }

    }
}