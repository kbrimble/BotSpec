using System;
using System.Collections.Generic;
using BotSpec.Assertions.Cards.CardComponents;
using BotSpec.Exceptions;
using BotSpec.Models.Cards;

namespace BotSpec.Assertions.Cards
{
    internal class ReceiptCardAssertions : IReceiptCardAssertions, IThrow<ReceiptCardAssertionFailedException>
    {
        private readonly ReceiptCard _receiptCard;
        private readonly StringHelpers<ReceiptCardAssertionFailedException> _stringHelpers;

        public ReceiptCardAssertions(ReceiptCard receiptCard) : this()
        {
            _receiptCard = receiptCard;
        }

        private ReceiptCardAssertions()
        {
            _stringHelpers = new StringHelpers<ReceiptCardAssertionFailedException>();
        }

        public IReceiptCardAssertions TitleMatching(string regex)
        {
            if (regex == null)
                throw new ArgumentNullException(nameof(regex));

            _stringHelpers.TestForMatch(_receiptCard.Title, regex, CreateEx(nameof(_receiptCard.Title), regex));

            return this;
        }

        public IReceiptCardAssertions TitleMatching(string regex, string groupMatchRegex, out IList<string> matchedGroups)
        {
            if (regex == null)
                throw new ArgumentNullException(nameof(regex));
            if (groupMatchRegex == null)
                throw new ArgumentNullException(nameof(groupMatchRegex));

            matchedGroups = _stringHelpers.TestForMatchAndReturnGroups(_receiptCard.Title, regex, groupMatchRegex, CreateEx(nameof(_receiptCard.Title), regex));

            return this;
        }

        public IReceiptCardAssertions TotalMatching(string regex)
        {
            if (regex == null)
                throw new ArgumentNullException(nameof(regex));

            _stringHelpers.TestForMatch(_receiptCard.Total, regex, CreateEx(nameof(_receiptCard.Total), regex));

            return this;
        }

        public IReceiptCardAssertions TotalMatching(string regex, string groupMatchRegex, out IList<string> matchedGroups)
        {
            if (regex == null)
                throw new ArgumentNullException(nameof(regex));
            if (groupMatchRegex == null)
                throw new ArgumentNullException(nameof(groupMatchRegex));

            matchedGroups = _stringHelpers.TestForMatchAndReturnGroups(_receiptCard.Total, regex, groupMatchRegex, CreateEx(nameof(_receiptCard.Total), regex));

            return this;
        }

        public IReceiptCardAssertions TaxMatching(string regex)
        {
            if (regex == null)
                throw new ArgumentNullException(nameof(regex));

            _stringHelpers.TestForMatch(_receiptCard.Tax, regex, CreateEx(nameof(_receiptCard.Tax), regex));

            return this;
        }

        public IReceiptCardAssertions TaxMatching(string regex, string groupMatchRegex, out IList<string> matchedGroups)
        {
            if (regex == null)
                throw new ArgumentNullException(nameof(regex));
            if (groupMatchRegex == null)
                throw new ArgumentNullException(nameof(groupMatchRegex));

            matchedGroups = _stringHelpers.TestForMatchAndReturnGroups(_receiptCard.Tax, regex, groupMatchRegex, CreateEx(nameof(_receiptCard.Tax), regex));

            return this;
        }

        public IReceiptCardAssertions VatMatching(string regex)
        {
            if (regex == null)
                throw new ArgumentNullException(nameof(regex));

            _stringHelpers.TestForMatch(_receiptCard.Vat, regex, CreateEx(nameof(_receiptCard.Vat), regex));

            return this;
        }

        public IReceiptCardAssertions VatMatching(string regex, string groupMatchRegex, out IList<string> matchedGroups)
        {
            if (regex == null)
                throw new ArgumentNullException(nameof(regex));
            if (groupMatchRegex == null)
                throw new ArgumentNullException(nameof(groupMatchRegex));

            matchedGroups = _stringHelpers.TestForMatchAndReturnGroups(_receiptCard.Vat, regex, groupMatchRegex, CreateEx(nameof(_receiptCard.Vat), regex));

            return this;
        }

        public ICardActionAssertions WithButtons()
        {
            return new CardActionSetAssertions(_receiptCard.Buttons);
        }

        public ICardActionAssertions WithTapAction()
        {
            return new CardActionAssertions(_receiptCard.Tap);
        }

        public IFactAssertions WithFact()
        {
            return new FactSetAssertions(_receiptCard);
        }

        public IReceiptItemAssertions WithReceiptItem()
        {
            return new ReceiptItemSetAssertions(_receiptCard);
        }

        public Func<ReceiptCardAssertionFailedException> CreateEx(string testedProperty, string regex)
        {
            var message = $"Expected card to have property {testedProperty} to match {regex} but regex test failed";
            return () => new ReceiptCardAssertionFailedException(message);
        }
    }
}
