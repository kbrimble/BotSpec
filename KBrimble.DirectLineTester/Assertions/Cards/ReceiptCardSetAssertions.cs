using System;
using System.Collections.Generic;
using KBrimble.DirectLineTester.Assertions.Cards.CardComponents;
using KBrimble.DirectLineTester.Exceptions;
using KBrimble.DirectLineTester.Models.Cards;

namespace KBrimble.DirectLineTester.Assertions.Cards
{
    public class ReceiptCardSetAssertions : IReceiptCardAssertions
    {
        private readonly IEnumerable<ReceiptCard> _receiptCards;
        private readonly SetHelpers<ReceiptCard, ReceiptCardAssertionFailedException> _setHelpers;

        public ReceiptCardSetAssertions(IEnumerable<ReceiptCard> receiptCards) : this()
        {
            _receiptCards = receiptCards;
        }

        private ReceiptCardSetAssertions()
        {
            _setHelpers = new SetHelpers<ReceiptCard, ReceiptCardAssertionFailedException>();
        }

        public IReceiptCardAssertions HasTitleMatching(string regex)
        {
            if (regex == null)
                throw new ArgumentNullException(nameof(regex));

            _setHelpers.TestSetForMatch(_receiptCards, card => card.That().HasTitleMatching(regex), CreateEx(nameof(ReceiptCard.Title), regex));

            return this;
        }

        public IReceiptCardAssertions HasTitleMatching(string regex, string groupMatchRegex, out IList<string> matchedGroups)
        {
            if (regex == null)
                throw new ArgumentNullException(nameof(regex));
            if (groupMatchRegex == null)
                throw new ArgumentNullException(nameof(groupMatchRegex));

            SetHelpers<ReceiptCard, ReceiptCardAssertionFailedException>.TestWithGroups act =
                (ReceiptCard item, out IList<string> matches) => item.That().HasTitleMatching(regex, groupMatchRegex, out matches);
            matchedGroups = _setHelpers.TestSetForMatchAndReturnGroups(_receiptCards, act, CreateEx(nameof(ReceiptCard.Title), regex));

            return this;
        }

        public IReceiptCardAssertions HasTotalMatching(string regex)
        {
            if (regex == null)
                throw new ArgumentNullException(nameof(regex));

            _setHelpers.TestSetForMatch(_receiptCards, card => card.That().HasTotalMatching(regex), CreateEx(nameof(ReceiptCard.Total), regex));

            return this;
        }

        public IReceiptCardAssertions HasTotalMatching(string regex, string groupMatchRegex, out IList<string> matchedGroups)
        {
            if (regex == null)
                throw new ArgumentNullException(nameof(regex));
            if (groupMatchRegex == null)
                throw new ArgumentNullException(nameof(groupMatchRegex));

            SetHelpers<ReceiptCard, ReceiptCardAssertionFailedException>.TestWithGroups act =
                (ReceiptCard item, out IList<string> matches) => item.That().HasTotalMatching(regex, groupMatchRegex, out matches);
            matchedGroups = _setHelpers.TestSetForMatchAndReturnGroups(_receiptCards, act, CreateEx(nameof(ReceiptCard.Total), regex));

            return this;
        }

        public IReceiptCardAssertions HasTaxMatching(string regex)
        {
            if (regex == null)
                throw new ArgumentNullException(nameof(regex));

            _setHelpers.TestSetForMatch(_receiptCards, card => card.That().HasTaxMatching(regex), CreateEx(nameof(ReceiptCard.Tax), regex));

            return this;
        }

        public IReceiptCardAssertions HasTaxMatching(string regex, string groupMatchRegex, out IList<string> matchedGroups)
        {
            if (regex == null)
                throw new ArgumentNullException(nameof(regex));
            if (groupMatchRegex == null)
                throw new ArgumentNullException(nameof(groupMatchRegex));

            SetHelpers<ReceiptCard, ReceiptCardAssertionFailedException>.TestWithGroups act =
                (ReceiptCard item, out IList<string> matches) => item.That().HasTaxMatching(regex, groupMatchRegex, out matches);
            matchedGroups = _setHelpers.TestSetForMatchAndReturnGroups(_receiptCards, act, CreateEx(nameof(ReceiptCard.Tax), regex));

            return this;
        }

        public IReceiptCardAssertions HasVatMatching(string regex)
        {
            if (regex == null)
                throw new ArgumentNullException(nameof(regex));

            _setHelpers.TestSetForMatch(_receiptCards, card => card.That().HasVatMatching(regex), CreateEx(nameof(ReceiptCard.Vat), regex));

            return this;
        }

        public IReceiptCardAssertions HasVatMatching(string regex, string groupMatchRegex, out IList<string> matchedGroups)
        {
            if (regex == null)
                throw new ArgumentNullException(nameof(regex));
            if (groupMatchRegex == null)
                throw new ArgumentNullException(nameof(groupMatchRegex));

            SetHelpers<ReceiptCard, ReceiptCardAssertionFailedException>.TestWithGroups act =
                (ReceiptCard item, out IList<string> matches) => item.That().HasVatMatching(regex, groupMatchRegex, out matches);
            matchedGroups = _setHelpers.TestSetForMatchAndReturnGroups(_receiptCards, act, CreateEx(nameof(ReceiptCard.Vat), regex));

            return this;
        }

        public ICardActionAssertions WithButtonsThat()
        {
            return new CardActionSetAssertions(_receiptCards as IEnumerable<IHaveButtons>);
        }

        public ICardActionAssertions WithTapActionThat()
        {
            return new CardActionSetAssertions(_receiptCards as IEnumerable<IHaveTapAction>);
        }

        public IReceiptItemAssertions WithReceiptItemThat()
        {
            return new ReceiptItemSetAssertions(_receiptCards);
        }

        public IFactAssertions WithFactThat()
        {
            return new FactSetAssertions(_receiptCards);
        }

        public Func<ReceiptCardAssertionFailedException> CreateEx(string testedProperty, string regex)
        {
            var message = $"Expected one receipt card to have property {testedProperty} to match regex {regex} but found none";
            return () => new ReceiptCardAssertionFailedException(message);
        }
    }
}