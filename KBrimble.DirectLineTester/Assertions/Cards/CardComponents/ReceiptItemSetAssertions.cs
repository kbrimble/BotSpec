using System;
using System.Collections.Generic;
using System.Linq;
using KBrimble.DirectLineTester.Exceptions;
using KBrimble.DirectLineTester.Models.Cards;

namespace KBrimble.DirectLineTester.Assertions.Cards.CardComponents
{
    public class ReceiptItemSetAssertions : IReceiptItemAssertions
    {
        public readonly IEnumerable<ReceiptItem> ReceiptItems;
        private readonly SetHelpers<ReceiptItem, ReceiptItemAssertionFailedException> _setHelpers;

        public ReceiptItemSetAssertions(ReceiptCard receiptCard) : this()
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

        public IReceiptItemAssertions HasTitleMatching(string regex)
        {
            if (regex == null)
                throw new ArgumentNullException(nameof(regex));

            _setHelpers.TestSetForMatch(ReceiptItems, item => item.That().HasTitleMatching(regex), CreateEx(nameof(ReceiptItem.Title), regex));

            return this;
        }

        public IReceiptItemAssertions HasTitleMatching(string regex, string groupMatchRegex, out IList<string> matchedGroups)
        {
            if (regex == null)
                throw new ArgumentNullException(nameof(regex));
            if (groupMatchRegex == null)
                throw new ArgumentNullException(nameof(groupMatchRegex));

            SetHelpers<ReceiptItem, ReceiptItemAssertionFailedException>.TestWithGroups act =
                (ReceiptItem item, out IList<string> matches) => item.That().HasTitleMatching(regex, groupMatchRegex, out matches);
            matchedGroups = _setHelpers.TestSetForMatchAndReturnGroups(ReceiptItems, act, CreateEx(nameof(ReceiptItem.Title), regex));

            return this;
        }

        public IReceiptItemAssertions HasSubtitleMatching(string regex)
        {
            if (regex == null)
                throw new ArgumentNullException(nameof(regex));

            _setHelpers.TestSetForMatch(ReceiptItems, item => item.That().HasSubtitleMatching(regex), CreateEx(nameof(ReceiptItem.Subtitle), regex));

            return this;
        }

        public IReceiptItemAssertions HasSubtitleMatching(string regex, string groupMatchRegex, out IList<string> matchedGroups)
        {
            if (regex == null)
                throw new ArgumentNullException(nameof(regex));
            if (groupMatchRegex == null)
                throw new ArgumentNullException(nameof(groupMatchRegex));

            SetHelpers<ReceiptItem, ReceiptItemAssertionFailedException>.TestWithGroups act =
                (ReceiptItem item, out IList<string> matches) => item.That().HasSubtitleMatching(regex, groupMatchRegex, out matches);
            matchedGroups = _setHelpers.TestSetForMatchAndReturnGroups(ReceiptItems, act, CreateEx(nameof(ReceiptItem.Subtitle), regex));

            return this;
        }

        public IReceiptItemAssertions HasTextMatching(string regex)
        {
            if (regex == null)
                throw new ArgumentNullException(nameof(regex));

            _setHelpers.TestSetForMatch(ReceiptItems, item => item.That().HasTextMatching(regex), CreateEx(nameof(ReceiptItem.Text), regex));

            return this;
        }

        public IReceiptItemAssertions HasTextMatching(string regex, string groupMatchRegex, out IList<string> matchedGroups)
        {
            if (regex == null)
                throw new ArgumentNullException(nameof(regex));
            if (groupMatchRegex == null)
                throw new ArgumentNullException(nameof(groupMatchRegex));

            SetHelpers<ReceiptItem, ReceiptItemAssertionFailedException>.TestWithGroups act =
                (ReceiptItem item, out IList<string> matches) => item.That().HasTextMatching(regex, groupMatchRegex, out matches);
            matchedGroups = _setHelpers.TestSetForMatchAndReturnGroups(ReceiptItems, act, CreateEx(nameof(ReceiptItem.Text), regex));

            return this;
        }

        public IReceiptItemAssertions HasPriceMatching(string regex)
        {
            if (regex == null)
                throw new ArgumentNullException(nameof(regex));

            _setHelpers.TestSetForMatch(ReceiptItems, item => item.That().HasPriceMatching(regex), CreateEx(nameof(ReceiptItem.Price), regex));

            return this;
        }

        public IReceiptItemAssertions HasPriceMatching(string regex, string groupMatchRegex, out IList<string> matchedGroups)
        {
            if (regex == null)
                throw new ArgumentNullException(nameof(regex));
            if (groupMatchRegex == null)
                throw new ArgumentNullException(nameof(groupMatchRegex));

            SetHelpers<ReceiptItem, ReceiptItemAssertionFailedException>.TestWithGroups act =
                (ReceiptItem item, out IList<string> matches) => item.That().HasPriceMatching(regex, groupMatchRegex, out matches);
            matchedGroups = _setHelpers.TestSetForMatchAndReturnGroups(ReceiptItems, act, CreateEx(nameof(ReceiptItem.Price), regex));

            return this;
        }

        public IReceiptItemAssertions HasQuantityMatching(string regex)
        {
            if (regex == null)
                throw new ArgumentNullException(nameof(regex));

            _setHelpers.TestSetForMatch(ReceiptItems, item => item.That().HasQuantityMatching(regex), CreateEx(nameof(ReceiptItem.Quantity), regex));

            return this;
        }

        public IReceiptItemAssertions HasQuantityMatching(string regex, string groupMatchRegex, out IList<string> matchedGroups)
        {
            if (regex == null)
                throw new ArgumentNullException(nameof(regex));
            if (groupMatchRegex == null)
                throw new ArgumentNullException(nameof(groupMatchRegex));

            SetHelpers<ReceiptItem, ReceiptItemAssertionFailedException>.TestWithGroups act =
                (ReceiptItem item, out IList<string> matches) => item.That().HasQuantityMatching(regex, groupMatchRegex, out matches);
            matchedGroups = _setHelpers.TestSetForMatchAndReturnGroups(ReceiptItems, act, CreateEx(nameof(ReceiptItem.Quantity), regex));

            return this;
        }

        public ICardActionAssertions WithTapActionThat()
        {
            return new CardActionSetAssertions(ReceiptItems);
        }

        public ICardImageAssertions WithCardImageThat()
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