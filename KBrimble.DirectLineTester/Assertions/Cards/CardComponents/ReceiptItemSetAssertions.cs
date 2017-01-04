using System;
using System.Collections.Generic;
using System.Linq;
using KBrimble.DirectLineTester.Exceptions;
using KBrimble.DirectLineTester.Models.Cards;

namespace KBrimble.DirectLineTester.Assertions.Cards.CardComponents
{
    internal class ReceiptItemSetAssertions : IReceiptItemAssertions, IThrow<ReceiptItemAssertionFailedException>
    {
        public readonly IEnumerable<ReceiptItem> ReceiptItems;
        private readonly SetHelpers<ReceiptItem, ReceiptItemAssertionFailedException> _setHelpers;

        internal ReceiptItemSetAssertions(ReceiptCard receiptCard) : this()
        {
            if (receiptCard == null)
                throw new ArgumentNullException(nameof(receiptCard));

            ReceiptItems = receiptCard.Items.Where(x => x != null);
        }

        public ReceiptItemSetAssertions(IEnumerable<ReceiptItem> items) : this()
        {
            if (items == null)
                throw new ArgumentNullException(nameof(items));

            ReceiptItems = items.Where(x => x != null);
        }

        public ReceiptItemSetAssertions(IEnumerable<IHaveReceiptItems> iHaveReceiptItemses) : this()
        {
            if (iHaveReceiptItemses == null)
                throw new ArgumentNullException(nameof(iHaveReceiptItemses));

            ReceiptItems = iHaveReceiptItemses.SelectMany(x => x?.Items).Where(x => x != null);
        }

        private ReceiptItemSetAssertions()
        {
            _setHelpers = new SetHelpers<ReceiptItem, ReceiptItemAssertionFailedException>();
        }

        public IReceiptItemAssertions TitleMatching(string regex)
        {
            if (regex == null)
                throw new ArgumentNullException(nameof(regex));

            _setHelpers.TestSetForMatch(ReceiptItems, item => item.That().TitleMatching(regex), CreateEx(nameof(ReceiptItem.Title), regex));

            return this;
        }

        public IReceiptItemAssertions TitleMatching(string regex, string groupMatchRegex, out IList<string> matchedGroups)
        {
            if (regex == null)
                throw new ArgumentNullException(nameof(regex));
            if (groupMatchRegex == null)
                throw new ArgumentNullException(nameof(groupMatchRegex));

            SetHelpers<ReceiptItem, ReceiptItemAssertionFailedException>.TestWithGroups act =
                (ReceiptItem item, out IList<string> matches) => item.That().TitleMatching(regex, groupMatchRegex, out matches);
            matchedGroups = _setHelpers.TestSetForMatchAndReturnGroups(ReceiptItems, act, CreateEx(nameof(ReceiptItem.Title), regex));

            return this;
        }

        public IReceiptItemAssertions SubtitleMatching(string regex)
        {
            if (regex == null)
                throw new ArgumentNullException(nameof(regex));

            _setHelpers.TestSetForMatch(ReceiptItems, item => item.That().SubtitleMatching(regex), CreateEx(nameof(ReceiptItem.Subtitle), regex));

            return this;
        }

        public IReceiptItemAssertions SubtitleMatching(string regex, string groupMatchRegex, out IList<string> matchedGroups)
        {
            if (regex == null)
                throw new ArgumentNullException(nameof(regex));
            if (groupMatchRegex == null)
                throw new ArgumentNullException(nameof(groupMatchRegex));

            SetHelpers<ReceiptItem, ReceiptItemAssertionFailedException>.TestWithGroups act =
                (ReceiptItem item, out IList<string> matches) => item.That().SubtitleMatching(regex, groupMatchRegex, out matches);
            matchedGroups = _setHelpers.TestSetForMatchAndReturnGroups(ReceiptItems, act, CreateEx(nameof(ReceiptItem.Subtitle), regex));

            return this;
        }

        public IReceiptItemAssertions TextMatching(string regex)
        {
            if (regex == null)
                throw new ArgumentNullException(nameof(regex));

            _setHelpers.TestSetForMatch(ReceiptItems, item => item.That().TextMatching(regex), CreateEx(nameof(ReceiptItem.Text), regex));

            return this;
        }

        public IReceiptItemAssertions TextMatching(string regex, string groupMatchRegex, out IList<string> matchedGroups)
        {
            if (regex == null)
                throw new ArgumentNullException(nameof(regex));
            if (groupMatchRegex == null)
                throw new ArgumentNullException(nameof(groupMatchRegex));

            SetHelpers<ReceiptItem, ReceiptItemAssertionFailedException>.TestWithGroups act =
                (ReceiptItem item, out IList<string> matches) => item.That().TextMatching(regex, groupMatchRegex, out matches);
            matchedGroups = _setHelpers.TestSetForMatchAndReturnGroups(ReceiptItems, act, CreateEx(nameof(ReceiptItem.Text), regex));

            return this;
        }

        public IReceiptItemAssertions PriceMatching(string regex)
        {
            if (regex == null)
                throw new ArgumentNullException(nameof(regex));

            _setHelpers.TestSetForMatch(ReceiptItems, item => item.That().PriceMatching(regex), CreateEx(nameof(ReceiptItem.Price), regex));

            return this;
        }

        public IReceiptItemAssertions PriceMatching(string regex, string groupMatchRegex, out IList<string> matchedGroups)
        {
            if (regex == null)
                throw new ArgumentNullException(nameof(regex));
            if (groupMatchRegex == null)
                throw new ArgumentNullException(nameof(groupMatchRegex));

            SetHelpers<ReceiptItem, ReceiptItemAssertionFailedException>.TestWithGroups act =
                (ReceiptItem item, out IList<string> matches) => item.That().PriceMatching(regex, groupMatchRegex, out matches);
            matchedGroups = _setHelpers.TestSetForMatchAndReturnGroups(ReceiptItems, act, CreateEx(nameof(ReceiptItem.Price), regex));

            return this;
        }

        public IReceiptItemAssertions QuantityMatching(string regex)
        {
            if (regex == null)
                throw new ArgumentNullException(nameof(regex));

            _setHelpers.TestSetForMatch(ReceiptItems, item => item.That().QuantityMatching(regex), CreateEx(nameof(ReceiptItem.Quantity), regex));

            return this;
        }

        public IReceiptItemAssertions QuantityMatching(string regex, string groupMatchRegex, out IList<string> matchedGroups)
        {
            if (regex == null)
                throw new ArgumentNullException(nameof(regex));
            if (groupMatchRegex == null)
                throw new ArgumentNullException(nameof(groupMatchRegex));

            SetHelpers<ReceiptItem, ReceiptItemAssertionFailedException>.TestWithGroups act =
                (ReceiptItem item, out IList<string> matches) => item.That().QuantityMatching(regex, groupMatchRegex, out matches);
            matchedGroups = _setHelpers.TestSetForMatchAndReturnGroups(ReceiptItems, act, CreateEx(nameof(ReceiptItem.Quantity), regex));

            return this;
        }

        public ICardActionAssertions WithTapAction()
        {
            return new CardActionSetAssertions(ReceiptItems);
        }

        public ICardImageAssertions WithCardImage()
        {
            return new CardImageSetAssertions(ReceiptItems);
        }

        public Func<ReceiptItemAssertionFailedException> CreateEx(string testedProperty, string regex)
        {
            var message = $"Expected one receipt item to have property {testedProperty} that matched {regex} but found none";
            return () => new ReceiptItemAssertionFailedException(message);
        }
    }
}