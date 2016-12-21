using System.Collections.Generic;

namespace KBrimble.DirectLineTester.Models.Cards
{
    public class ReceiptCard : IHaveButtons, IHaveTapAction, IHaveFacts, IHaveReceiptItems
    {
        public const string ContentType = "application/vnd.microsoft.card.receipt";

        /// <summary>
        /// Initializes a new instance of the ReceiptCard class.
        /// </summary>
        public ReceiptCard() { }

        /// <summary>
        /// Initializes a new instance of the ReceiptCard class.
        /// </summary>
        public ReceiptCard(string title = default(string), IList<ReceiptItem> items = default(IList<ReceiptItem>), IList<Fact> facts = default(IList<Fact>), CardAction tap = default(CardAction), string total = default(string), string tax = default(string), string vat = default(string), IList<CardAction> buttons = default(IList<CardAction>))
        {
            Title = title;
            Items = items;
            Facts = facts;
            Tap = tap;
            Total = total;
            Tax = tax;
            Vat = vat;
            Buttons = buttons;
        }

        /// <summary>
        /// Title of the card
        /// </summary>
        public string Title { get; set; }
        
        /// <summary>
        /// Total amount of money paid (or should be paid)
        /// </summary>
        public string Total { get; set; }

        /// <summary>
        /// Total amount of TAX paid(or should be paid)
        /// </summary>
        public string Tax { get; set; }

        /// <summary>
        /// Total amount of VAT paid(or should be paid)
        /// </summary>
        public string Vat { get; set; }

        /// <inheritdoc/>
        public IList<ReceiptItem> Items { get; set; }

        /// <inheritdoc/>
        public IList<Fact> Facts { get; set; }

        /// <inheritdoc/>
        public CardAction Tap { get; set; }

        /// <inheritdoc/>
        public IList<CardAction> Buttons { get; set; }

    }
}
