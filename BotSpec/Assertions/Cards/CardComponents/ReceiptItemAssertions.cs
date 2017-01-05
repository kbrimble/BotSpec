using System;
using System.Collections.Generic;
using BotSpec.Exceptions;
using BotSpec.Models.Cards;

namespace BotSpec.Assertions.Cards.CardComponents
{
    public class ReceiptItemAssertions : IReceiptItemAssertions, IThrow<ReceiptItemAssertionFailedException>
    {
        private readonly ReceiptItem _receiptItem;
        private readonly StringHelpers<ReceiptItemAssertionFailedException> _stringHelpers;

        internal ReceiptItemAssertions(ReceiptItem receiptItem)
        {
            if (receiptItem == null)
                throw new ArgumentNullException(nameof(receiptItem));

            _receiptItem = receiptItem;
            _stringHelpers = new StringHelpers<ReceiptItemAssertionFailedException>();
        }

        public IReceiptItemAssertions TitleMatching(string regex)
        {
            if (regex == null)
                throw new ArgumentNullException(nameof(regex));

            _stringHelpers.TestForMatch(_receiptItem.Title, regex, CreateEx(nameof(_receiptItem.Title), regex));

            return this;
        }

        public IReceiptItemAssertions TitleMatching(string regex, string groupMatchRegex, out IList<string> matchedGroups)
        {
            if (regex == null)
                throw new ArgumentNullException(nameof(regex));
            if (groupMatchRegex == null)
                throw new ArgumentNullException(nameof(groupMatchRegex));

            matchedGroups = _stringHelpers.TestForMatchAndReturnGroups(_receiptItem.Title, regex, groupMatchRegex, CreateEx(nameof(_receiptItem.Title), regex));

            return this;
        }

        public IReceiptItemAssertions SubtitleMatching(string regex)
        {
            if (regex == null)
                throw new ArgumentNullException(nameof(regex));

            _stringHelpers.TestForMatch(_receiptItem.Subtitle, regex, CreateEx(nameof(_receiptItem.Subtitle), regex));

            return this;
        }

        public IReceiptItemAssertions SubtitleMatching(string regex, string groupMatchRegex, out IList<string> matchedGroups)
        {
            if (regex == null)
                throw new ArgumentNullException(nameof(regex));
            if (groupMatchRegex == null)
                throw new ArgumentNullException(nameof(groupMatchRegex));

            matchedGroups = _stringHelpers.TestForMatchAndReturnGroups(_receiptItem.Subtitle, regex, groupMatchRegex, CreateEx(nameof(_receiptItem.Subtitle), regex));

            return this;
        }

        public IReceiptItemAssertions TextMatching(string regex)
        {
            if (regex == null)
                throw new ArgumentNullException(nameof(regex));

            _stringHelpers.TestForMatch(_receiptItem.Text, regex, CreateEx(nameof(_receiptItem.Text), regex));

            return this;
        }

        public IReceiptItemAssertions TextMatching(string regex, string groupMatchRegex, out IList<string> matchedGroups)
        {
            if (regex == null)
                throw new ArgumentNullException(nameof(regex));
            if (groupMatchRegex == null)
                throw new ArgumentNullException(nameof(groupMatchRegex));

            matchedGroups = _stringHelpers.TestForMatchAndReturnGroups(_receiptItem.Text, regex, groupMatchRegex, CreateEx(nameof(_receiptItem.Text), regex));

            return this;
        }

        public IReceiptItemAssertions PriceMatching(string regex)
        {
            if (regex == null)
                throw new ArgumentNullException(nameof(regex));

            _stringHelpers.TestForMatch(_receiptItem.Price, regex, CreateEx(nameof(_receiptItem.Price), regex));

            return this;
        }

        public IReceiptItemAssertions PriceMatching(string regex, string groupMatchRegex, out IList<string> matchedGroups)
        {
            if (regex == null)
                throw new ArgumentNullException(nameof(regex));
            if (groupMatchRegex == null)
                throw new ArgumentNullException(nameof(groupMatchRegex));

            matchedGroups = _stringHelpers.TestForMatchAndReturnGroups(_receiptItem.Price, regex, groupMatchRegex, CreateEx(nameof(_receiptItem.Price), regex));

            return this;
        }

        public IReceiptItemAssertions QuantityMatching(string regex)
        {
            if (regex == null)
                throw new ArgumentNullException(nameof(regex));

            _stringHelpers.TestForMatch(_receiptItem.Quantity, regex, CreateEx(nameof(_receiptItem.Quantity), regex));

            return this;
        }

        public IReceiptItemAssertions QuantityMatching(string regex, string groupMatchRegex, out IList<string> matchedGroups)
        {
            if (regex == null)
                throw new ArgumentNullException(nameof(regex));
            if (groupMatchRegex == null)
                throw new ArgumentNullException(nameof(groupMatchRegex));

            matchedGroups = _stringHelpers.TestForMatchAndReturnGroups(_receiptItem.Quantity, regex, groupMatchRegex, CreateEx(nameof(_receiptItem.Quantity), regex));

            return this;
        }

        public ICardActionAssertions WithTapAction()
        {
            return new CardActionAssertions(_receiptItem.Tap);
        }

        public ICardImageAssertions WithCardImage()
        {
            return new CardImageAssertions(_receiptItem.Image);
        }

        public Func<ReceiptItemAssertionFailedException> CreateEx(string testedProperty, string regex)
        {
            var message = $"Expected property {testedProperty} to match {regex} but did not";
            return () => new ReceiptItemAssertionFailedException(message);
        }
    }
}