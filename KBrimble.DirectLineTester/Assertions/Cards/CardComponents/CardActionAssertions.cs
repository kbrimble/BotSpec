using System;
using System.Collections.Generic;
using KBrimble.DirectLineTester.Exceptions;
using KBrimble.DirectLineTester.Models.Cards;

namespace KBrimble.DirectLineTester.Assertions.Cards.CardComponents
{
    public class CardActionAssertions : ICardActionAssertions, IThrow<CardActionAssertionFailedException>
    {
        private readonly CardAction _cardAction;

        public CardActionAssertions(CardAction cardAction)
        {
            if (cardAction == null)
                throw new ArgumentNullException(nameof(cardAction));

            _cardAction = cardAction;
        }

        public ICardActionAssertions HasTitleMatching(string regex)
        {
            if (regex == null)
                throw new ArgumentNullException(nameof(regex));

            StringHelpers.TestForMatch(_cardAction.Title, regex, CreateEx(nameof(_cardAction.Title), regex));

            return this;
        }

        public ICardActionAssertions HasTitleMatching(string regex, string groupMatchingRegex, out IList<string> groupMatches)
        {
            if (regex == null)
                throw new ArgumentNullException(nameof(regex));
            if (groupMatchingRegex == null)
                throw new ArgumentNullException(nameof(groupMatchingRegex));

            groupMatches = StringHelpers.TestForMatchAndReturnGroups(_cardAction.Title, regex, groupMatchingRegex, CreateEx(nameof(_cardAction.Title), regex));

            return this;
        }

        public ICardActionAssertions HasValueMatching(string regex)
        {
            if (regex == null)
                throw new ArgumentNullException(nameof(regex));

            StringHelpers.TestForMatch(_cardAction.Value, regex, CreateEx(nameof(_cardAction.Value), regex));

            return this;
        }

        public ICardActionAssertions HasValueMatching(string regex, string groupMatchingRegex, out IList<string> groupMatches)
        {
            if (regex == null)
                throw new ArgumentNullException(nameof(regex));
            if (groupMatchingRegex == null)
                throw new ArgumentNullException(nameof(groupMatchingRegex));

            groupMatches = StringHelpers.TestForMatchAndReturnGroups(_cardAction.Value, regex, groupMatchingRegex, CreateEx(nameof(_cardAction.Value), regex));

            return this;
        }

        public ICardActionAssertions HasType(CardActionType type)
        {
            if (type == null)
                throw new ArgumentNullException(nameof(type));

            StringHelpers.TestForMatch(_cardAction.Type, type.Value, CreateEx(nameof(_cardAction.Type), type.Value));

            return this;
        }

        public ICardActionAssertions HasImageMatching(string regex)
        {
            if (regex == null)
                throw new ArgumentNullException(nameof(regex));

            StringHelpers.TestForMatch(_cardAction.Image, regex, CreateEx(nameof(_cardAction.Image), regex));

            return this;
        }

        public ICardActionAssertions HasImageMatching(string regex, string groupMatchingRegex, out IList<string> groupMatches)
        {
            if (regex == null)
                throw new ArgumentNullException(nameof(regex));
            if (groupMatchingRegex == null)
                throw new ArgumentNullException(nameof(groupMatchingRegex));

            groupMatches = StringHelpers.TestForMatchAndReturnGroups(_cardAction.Image, regex, groupMatchingRegex, CreateEx(nameof(_cardAction.Image), regex));

            return this;
        }

        public CardActionAssertionFailedException CreateEx(string testedProperty, string regex)
        {
            var message = $"Expected card action to have property {testedProperty} matching {regex} but it did not.";
            return new CardActionAssertionFailedException(message);
        }
    }
}
