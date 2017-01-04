using System;
using System.Collections.Generic;
using KBrimble.DirectLineTester.Assertions.Cards.CardComponents;
using KBrimble.DirectLineTester.Attachments;
using KBrimble.DirectLineTester.Exceptions;
using KBrimble.DirectLineTester.Models.Cards;
using Microsoft.Bot.Connector.DirectLine.Models;

namespace KBrimble.DirectLineTester.Assertions.Cards
{
    internal class ReceiptCardSetAssertions : IReceiptCardAssertions, IThrow<ReceiptCardAssertionFailedException>
    {
        public readonly IEnumerable<ReceiptCard> ReceiptCards;
        private readonly SetHelpers<ReceiptCard, ReceiptCardAssertionFailedException> _setHelpers;

        public ReceiptCardSetAssertions(Message message) : this()
        {
            var attachmentExtractor = AttachmentExtractorFactory.GetAttachmentExtractor();
            ReceiptCards = attachmentExtractor.ExtractCards<ReceiptCard>(message);
        }

        public ReceiptCardSetAssertions(IEnumerable<Message> messageSet) : this()
        {
            var attachmentExtractor = AttachmentExtractorFactory.GetAttachmentExtractor();
            ReceiptCards = attachmentExtractor.ExtractCards<ReceiptCard>(messageSet);
        }

        public ReceiptCardSetAssertions(IEnumerable<ReceiptCard> signinCards) : this()
        {
            ReceiptCards = signinCards;
        }

        private ReceiptCardSetAssertions()
        {
            _setHelpers = new SetHelpers<ReceiptCard, ReceiptCardAssertionFailedException>();
        }

        public IReceiptCardAssertions TitleMatching(string regex)
        {
            if (regex == null)
                throw new ArgumentNullException(nameof(regex));

            _setHelpers.TestSetForMatch(ReceiptCards, card => card.That().TitleMatching(regex), CreateEx(nameof(ReceiptCard.Title), regex));

            return this;
        }

        public IReceiptCardAssertions TitleMatching(string regex, string groupMatchRegex, out IList<string> matchedGroups)
        {
            if (regex == null)
                throw new ArgumentNullException(nameof(regex));
            if (groupMatchRegex == null)
                throw new ArgumentNullException(nameof(groupMatchRegex));

            SetHelpers<ReceiptCard, ReceiptCardAssertionFailedException>.TestWithGroups act =
                (ReceiptCard item, out IList<string> matches) => item.That().TitleMatching(regex, groupMatchRegex, out matches);
            matchedGroups = _setHelpers.TestSetForMatchAndReturnGroups(ReceiptCards, act, CreateEx(nameof(ReceiptCard.Title), regex));

            return this;
        }

        public IReceiptCardAssertions TotalMatching(string regex)
        {
            if (regex == null)
                throw new ArgumentNullException(nameof(regex));

            _setHelpers.TestSetForMatch(ReceiptCards, card => card.That().TotalMatching(regex), CreateEx(nameof(ReceiptCard.Total), regex));

            return this;
        }

        public IReceiptCardAssertions TotalMatching(string regex, string groupMatchRegex, out IList<string> matchedGroups)
        {
            if (regex == null)
                throw new ArgumentNullException(nameof(regex));
            if (groupMatchRegex == null)
                throw new ArgumentNullException(nameof(groupMatchRegex));

            SetHelpers<ReceiptCard, ReceiptCardAssertionFailedException>.TestWithGroups act =
                (ReceiptCard item, out IList<string> matches) => item.That().TotalMatching(regex, groupMatchRegex, out matches);
            matchedGroups = _setHelpers.TestSetForMatchAndReturnGroups(ReceiptCards, act, CreateEx(nameof(ReceiptCard.Total), regex));

            return this;
        }

        public IReceiptCardAssertions TaxMatching(string regex)
        {
            if (regex == null)
                throw new ArgumentNullException(nameof(regex));

            _setHelpers.TestSetForMatch(ReceiptCards, card => card.That().TaxMatching(regex), CreateEx(nameof(ReceiptCard.Tax), regex));

            return this;
        }

        public IReceiptCardAssertions TaxMatching(string regex, string groupMatchRegex, out IList<string> matchedGroups)
        {
            if (regex == null)
                throw new ArgumentNullException(nameof(regex));
            if (groupMatchRegex == null)
                throw new ArgumentNullException(nameof(groupMatchRegex));

            SetHelpers<ReceiptCard, ReceiptCardAssertionFailedException>.TestWithGroups act =
                (ReceiptCard item, out IList<string> matches) => item.That().TaxMatching(regex, groupMatchRegex, out matches);
            matchedGroups = _setHelpers.TestSetForMatchAndReturnGroups(ReceiptCards, act, CreateEx(nameof(ReceiptCard.Tax), regex));

            return this;
        }

        public IReceiptCardAssertions VatMatching(string regex)
        {
            if (regex == null)
                throw new ArgumentNullException(nameof(regex));

            _setHelpers.TestSetForMatch(ReceiptCards, card => card.That().VatMatching(regex), CreateEx(nameof(ReceiptCard.Vat), regex));

            return this;
        }

        public IReceiptCardAssertions VatMatching(string regex, string groupMatchRegex, out IList<string> matchedGroups)
        {
            if (regex == null)
                throw new ArgumentNullException(nameof(regex));
            if (groupMatchRegex == null)
                throw new ArgumentNullException(nameof(groupMatchRegex));

            SetHelpers<ReceiptCard, ReceiptCardAssertionFailedException>.TestWithGroups act =
                (ReceiptCard item, out IList<string> matches) => item.That().VatMatching(regex, groupMatchRegex, out matches);
            matchedGroups = _setHelpers.TestSetForMatchAndReturnGroups(ReceiptCards, act, CreateEx(nameof(ReceiptCard.Vat), regex));

            return this;
        }

        public ICardActionAssertions WithButtons()
        {
            return new CardActionSetAssertions(ReceiptCards as IEnumerable<IHaveButtons>);
        }

        public ICardActionAssertions WithTapAction()
        {
            return new CardActionSetAssertions(ReceiptCards as IEnumerable<IHaveTapAction>);
        }

        public IReceiptItemAssertions WithReceiptItem()
        {
            return new ReceiptItemSetAssertions(ReceiptCards);
        }

        public IFactAssertions WithFact()
        {
            return new FactSetAssertions(ReceiptCards);
        }

        public Func<ReceiptCardAssertionFailedException> CreateEx(string testedProperty, string regex)
        {
            var message = $"Expected one receipt card to have property {testedProperty} to match regex {regex} but found none";
            return () => new ReceiptCardAssertionFailedException(message);
        }
    }
}