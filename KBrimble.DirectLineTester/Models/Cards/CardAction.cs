namespace KBrimble.DirectLineTester
{
    /// <summary>
    /// An action on a card
    /// </summary>
    public class CardAction
    {
        /// <summary>
        /// Defines the type of action implemented by this button.
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Text description which appear on the button.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// URL Picture which will appear on the button, next to text label.
        /// </summary>
        public string Image { get; set; }

        /// <summary>
        /// Supplementary parameter for action. Content of this property
        /// depends on the ActionType
        /// </summary>
        public string Value { get; set; }

    }
}
