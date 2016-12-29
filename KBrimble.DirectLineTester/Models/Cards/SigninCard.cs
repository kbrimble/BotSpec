using System.Collections.Generic;

namespace KBrimble.DirectLineTester.Models.Cards
{
    /// <summary>
    /// A card representing a request to sign in
    /// </summary>
    public class SigninCard : IHaveButtons
    {
        public const string ContentType = "application/vnd.microsoft.card.signin";

        /// <summary>
        /// Initializes a new instance of the SigninCard class.
        /// </summary>
        public SigninCard() { }

        /// <summary>
        /// Initializes a new instance of the SigninCard class.
        /// </summary>
        public SigninCard(string text = default(string), IList<CardAction> buttons = default(IList<CardAction>))
        {
            Text = text;
            Buttons = buttons;
        }

        /// <summary>
        /// Text for signin request
        /// </summary>
        public string Text { get; set; }

        /// <inheritdoc />
        public IList<CardAction> Buttons { get; set; }
    }
}
