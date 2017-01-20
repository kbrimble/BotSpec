using System;
using System.Collections.Generic;
using BotSpec.Exceptions;
using BotSpec.Models;
using Microsoft.Bot.Connector.DirectLine;

namespace BotSpec.Assertions.Cards.CardComponents
{
    internal class CardActionAssertions : ICardActionAssertions, IThrow<CardActionAssertionFailedException>
    {
        private readonly CardAction _cardAction;
        private readonly StringHelpers<CardActionAssertionFailedException> _stringHelpers;

        public CardActionAssertions(CardAction cardAction)
        {
            if (cardAction == null)
                throw new ArgumentNullException(nameof(cardAction));

            _cardAction = cardAction;
            _stringHelpers = new StringHelpers<CardActionAssertionFailedException>();
        }

        public ICardActionAssertions TitleMatching(string regex)
        {
            if (regex == null)
                throw new ArgumentNullException(nameof(regex));

            _stringHelpers.TestForMatch(_cardAction.Title, regex, CreateEx(nameof(_cardAction.Title), regex));

            return this;
        }

        public ICardActionAssertions TitleMatching(string regex, string groupMatchingRegex, out IList<string> groupMatches)
        {
            if (regex == null)
                throw new ArgumentNullException(nameof(regex));
            if (groupMatchingRegex == null)
                throw new ArgumentNullException(nameof(groupMatchingRegex));

            groupMatches = _stringHelpers.TestForMatchAndReturnGroups(_cardAction.Title, regex, groupMatchingRegex, CreateEx(nameof(_cardAction.Title), regex));

            return this;
        }

        public ICardActionAssertions ValueMatching(string regex)
        {
            if (regex == null)
                throw new ArgumentNullException(nameof(regex));

            _stringHelpers.TestForMatch(_cardAction?.Value?.ToString(), regex, CreateEx(nameof(_cardAction.Value), regex));

            return this;
        }

        public ICardActionAssertions ValueMatching(string regex, string groupMatchingRegex, out IList<string> groupMatches)
        {
            if (regex == null)
                throw new ArgumentNullException(nameof(regex));
            if (groupMatchingRegex == null)
                throw new ArgumentNullException(nameof(groupMatchingRegex));

            groupMatches = _stringHelpers.TestForMatchAndReturnGroups(_cardAction?.Value?.ToString(), regex, groupMatchingRegex, CreateEx(nameof(_cardAction.Value), regex));

            return this;
        }

        public ICardActionAssertions ActionType(CardActionType type)
        {
            _stringHelpers.TestForMatch(_cardAction.Type, CardActionTypeMap.Map(type), CreateEx(nameof(_cardAction.Type), type.ToString()));

            return this;
        }

        public ICardActionAssertions ImageMatching(string regex)
        {
            if (regex == null)
                throw new ArgumentNullException(nameof(regex));

            _stringHelpers.TestForMatch(_cardAction.Image, regex, CreateEx(nameof(_cardAction.Image), regex));

            return this;
        }

        public ICardActionAssertions ImageMatching(string regex, string groupMatchingRegex, out IList<string> groupMatches)
        {
            if (regex == null)
                throw new ArgumentNullException(nameof(regex));
            if (groupMatchingRegex == null)
                throw new ArgumentNullException(nameof(groupMatchingRegex));

            groupMatches = _stringHelpers.TestForMatchAndReturnGroups(_cardAction.Image, regex, groupMatchingRegex, CreateEx(nameof(_cardAction.Image), regex));

            return this;
        }

        public Func<CardActionAssertionFailedException> CreateEx(string testedProperty, string regex)
        {
            var message = $"Expected card action to have property {testedProperty} matching {regex} but it did not.";
            return () => new CardActionAssertionFailedException(message);
        }
    }
}
