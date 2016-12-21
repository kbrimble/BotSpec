using System;
using System.Collections.Generic;
using System.Linq;
using KBrimble.DirectLineTester.Exceptions;
using KBrimble.DirectLineTester.Models.Cards;

namespace KBrimble.DirectLineTester.Assertions.Cards.CardComponents
{
    public class CardActionSetAssertions : ICardActionAssertions
    {
        public readonly IList<CardAction> CardActions;
        private readonly SetHelpers<CardAction, CardActionAssertionFailedException> _setHelpers;

        public CardActionSetAssertions(IList<CardAction> cardActions)
        {
            if (cardActions == null)
                throw new ArgumentNullException(nameof(cardActions));

            CardActions = cardActions.Where(x => x != null).ToList();
            _setHelpers = new SetHelpers<CardAction, CardActionAssertionFailedException>();
        }

        public CardActionSetAssertions(IEnumerable<IHaveTapAction> hasTapActions)
        {
            if (hasTapActions == null)
                throw new ArgumentNullException(nameof(hasTapActions));

            CardActions = hasTapActions.Where(x => x?.Tap != null).Select(x => x.Tap).ToList();
        }

        public CardActionSetAssertions(IEnumerable<IHaveButtons> hasButtons)
        {
            if (hasButtons == null)
                throw new ArgumentNullException(nameof(hasButtons));

            CardActions = hasButtons.Where(x => x?.Buttons != null).Select(x => x.Buttons).SelectMany(x => x).ToList();
        }

        public ICardActionAssertions HasTitleMatching(string regex)
        {
            if (regex == null)
                throw new ArgumentNullException(nameof(regex));

            _setHelpers.TestSetForMatch(CardActions, action => action.That().HasTitleMatching(regex), CreateEx(nameof(CardAction.Title), regex));

            return this;
        }

        public ICardActionAssertions HasTitleMatching(string regex, string groupMatchingRegex, out IList<string> groupMatches)
        {
            if (regex == null)
                throw new ArgumentNullException(nameof(regex));
            if (groupMatchingRegex == null)
                throw new ArgumentNullException(nameof(groupMatchingRegex));

            SetHelpers<CardAction, CardActionAssertionFailedException>.TestWithGroups act
                = (CardAction item, out IList<string> matches) => item.That().HasTitleMatching(regex, groupMatchingRegex, out matches);

            groupMatches = _setHelpers.TestSetForMatchAndReturnGroups(CardActions, act, CreateEx(nameof(CardAction.Title), regex));

            return this;
        }

        public ICardActionAssertions HasValueMatching(string regex)
        {
            if (regex == null)
                throw new ArgumentNullException(nameof(regex));

            _setHelpers.TestSetForMatch(CardActions, action => action.That().HasValueMatching(regex), CreateEx(nameof(CardAction.Value), regex));

            return this;
        }

        public ICardActionAssertions HasValueMatching(string regex, string groupMatchingRegex, out IList<string> groupMatches)
        {
            if (regex == null)
                throw new ArgumentNullException(nameof(regex));
            if (groupMatchingRegex == null)
                throw new ArgumentNullException(nameof(groupMatchingRegex));

            SetHelpers<CardAction, CardActionAssertionFailedException>.TestWithGroups act
                = (CardAction item, out IList<string> matches) => item.That().HasValueMatching(regex, groupMatchingRegex, out matches);

            groupMatches = _setHelpers.TestSetForMatchAndReturnGroups(CardActions, act, CreateEx(nameof(CardAction.Title), regex));

            return this;
        }

        public ICardActionAssertions HasImageMatching(string regex)
        {
            if (regex == null)
                throw new ArgumentNullException(nameof(regex));

            _setHelpers.TestSetForMatch(CardActions, action => action.That().HasImageMatching(regex), CreateEx(nameof(CardAction.Image), regex));

            return this;
        }

        public ICardActionAssertions HasImageMatching(string regex, string groupMatchingRegex, out IList<string> groupMatches)
        {
            if (regex == null)
                throw new ArgumentNullException(nameof(regex));
            if (groupMatchingRegex == null)
                throw new ArgumentNullException(nameof(groupMatchingRegex));

            SetHelpers<CardAction, CardActionAssertionFailedException>.TestWithGroups act
                = (CardAction item, out IList<string> matches) => item.That().HasImageMatching(regex, groupMatchingRegex, out matches);

            groupMatches = _setHelpers.TestSetForMatchAndReturnGroups(CardActions, act, CreateEx(nameof(CardAction.Title), regex));

            return this;
        }

        public ICardActionAssertions HasType(CardActionType type)
        {
            if (type == null)
                throw new ArgumentNullException(nameof(type));

            _setHelpers.TestSetForMatch(CardActions, action => action.That().HasType(type), CreateEx(nameof(CardAction.Type), type.Value));

            return this;
        }

        public Func<CardActionAssertionFailedException> CreateEx(string testedProperty, string regex)
        {
            var message = $"Expected at least one card action to have property {testedProperty} matching {regex} but none did.";
            return () => new CardActionAssertionFailedException(message);
        }
    }
}