using System;
using System.Collections.Generic;
using KBrimble.DirectLineTester.Exceptions;
using KBrimble.DirectLineTester.Models.Cards;

namespace KBrimble.DirectLineTester.Assertions.Cards
{
    public class CardActionSetAssertions : ICardActionAssertions, IThrow<CardActionSetAssertionFailedException>
    {
        private readonly IList<CardAction> _cardActions;
        private SetHelpers<CardAction, CardActionAssertionFailedException, CardActionSetAssertionFailedException> _setHelpers;

        public CardActionSetAssertions(IList<CardAction> cardActions)
        {
            _cardActions = cardActions;
            _setHelpers = new SetHelpers<CardAction, CardActionAssertionFailedException, CardActionSetAssertionFailedException>();
        }

        public ICardActionAssertions HasTitleMatching(string regex)
        {
            if (regex == null)
                throw new ArgumentNullException(nameof(regex));

            _setHelpers.TestSetForMatch(_cardActions, action => action.That().HasTitleMatching(regex), CreateEx(nameof(CardAction.Title), regex));

            return this;
        }

        public ICardActionAssertions HasTitleMatching(string regex, string groupMatchingRegex, out IList<string> groupMatches)
        {
            if (regex == null)
                throw new ArgumentNullException(nameof(regex));
            if (groupMatchingRegex == null)
                throw new ArgumentNullException(nameof(groupMatchingRegex));

            SetHelpers<CardAction, CardActionAssertionFailedException, CardActionSetAssertionFailedException>.TestWithGroups act
                = (CardAction item, out IList<string> matches) => item.That().HasTitleMatching(regex, groupMatchingRegex, out matches);

            groupMatches = _setHelpers.TestSetForMatchAndReturnGroups(_cardActions, act, CreateEx(nameof(CardAction.Title), regex));

            return this;
        }

        public ICardActionAssertions HasValueMatching(string regex)
        {
            if (regex == null)
                throw new ArgumentNullException(nameof(regex));

            _setHelpers.TestSetForMatch(_cardActions, action => action.That().HasValueMatching(regex), CreateEx(nameof(CardAction.Value), regex));

            return this;
        }

        public ICardActionAssertions HasValueMatching(string regex, string groupMatchingRegex, out IList<string> groupMatches)
        {
            if (regex == null)
                throw new ArgumentNullException(nameof(regex));
            if (groupMatchingRegex == null)
                throw new ArgumentNullException(nameof(groupMatchingRegex));

            SetHelpers<CardAction, CardActionAssertionFailedException, CardActionSetAssertionFailedException>.TestWithGroups act
                = (CardAction item, out IList<string> matches) => item.That().HasValueMatching(regex, groupMatchingRegex, out matches);

            groupMatches = _setHelpers.TestSetForMatchAndReturnGroups(_cardActions, act, CreateEx(nameof(CardAction.Title), regex));

            return this;
        }

        public ICardActionAssertions HasTypeMatching(string regex)
        {
            if (regex == null)
                throw new ArgumentNullException(nameof(regex));

            _setHelpers.TestSetForMatch(_cardActions, action => action.That().HasTypeMatching(regex), CreateEx(nameof(CardAction.Type), regex));

            return this;
        }

        public ICardActionAssertions HasTypeMatching(string regex, string groupMatchingRegex, out IList<string> groupMatches)
        {
            if (regex == null)
                throw new ArgumentNullException(nameof(regex));
            if (groupMatchingRegex == null)
                throw new ArgumentNullException(nameof(groupMatchingRegex));

            SetHelpers<CardAction, CardActionAssertionFailedException, CardActionSetAssertionFailedException>.TestWithGroups act
                = (CardAction item, out IList<string> matches) => item.That().HasTypeMatching(regex, groupMatchingRegex, out matches);

            groupMatches = _setHelpers.TestSetForMatchAndReturnGroups(_cardActions, act, CreateEx(nameof(CardAction.Title), regex));

            return this;
        }

        public ICardActionAssertions HasImageMatching(string regex)
        {
            if (regex == null)
                throw new ArgumentNullException(nameof(regex));

            _setHelpers.TestSetForMatch(_cardActions, action => action.That().HasImageMatching(regex), CreateEx(nameof(CardAction.Image), regex));

            return this;
        }

        public ICardActionAssertions HasImageMatching(string regex, string groupMatchingRegex, out IList<string> groupMatches)
        {
            if (regex == null)
                throw new ArgumentNullException(nameof(regex));
            if (groupMatchingRegex == null)
                throw new ArgumentNullException(nameof(groupMatchingRegex));

            SetHelpers<CardAction, CardActionAssertionFailedException, CardActionSetAssertionFailedException>.TestWithGroups act
                = (CardAction item, out IList<string> matches) => item.That().HasImageMatching(regex, groupMatchingRegex, out matches);

            groupMatches = _setHelpers.TestSetForMatchAndReturnGroups(_cardActions, act, CreateEx(nameof(CardAction.Title), regex));

            return this;
        }

        public CardActionSetAssertionFailedException CreateEx(string testedProperty, string regex)
        {
            var message = $"Expected at least one card action to have property {testedProperty} matching {regex} but none did.";
            return new CardActionSetAssertionFailedException(message);
        }
    }
}