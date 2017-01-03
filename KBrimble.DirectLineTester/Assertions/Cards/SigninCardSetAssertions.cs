using System;
using System.Collections.Generic;
using KBrimble.DirectLineTester.Assertions.Cards.CardComponents;
using KBrimble.DirectLineTester.Exceptions;
using KBrimble.DirectLineTester.Models.Cards;

namespace KBrimble.DirectLineTester.Assertions.Cards
{
    public class SigninCardSetAssertions : ISigninCardAssertions
    {
        public readonly IEnumerable<SigninCard> SigninCards;
        private readonly SetHelpers<SigninCard, SigninCardAssertionFailedException> _setHelpers;

        public SigninCardSetAssertions(IEnumerable<SigninCard> signinCards)
        {
            SigninCards = signinCards;
            _setHelpers = new SetHelpers<SigninCard, SigninCardAssertionFailedException>();
        }

        public ISigninCardAssertions HasTextMatching(string regex)
        {
            if (regex == null)
                throw new ArgumentNullException(nameof(regex));

            _setHelpers.TestSetForMatch(SigninCards, card => card.That().HasTextMatching(regex), CreateEx(nameof(SigninCard.Text), regex));

            return this;
        }

        public ISigninCardAssertions HasTextMatching(string regex, string groupMatchRegex, out IList<string> matchedGroups)
        {
            if (regex == null)
                throw new ArgumentNullException(nameof(regex));
            if (groupMatchRegex == null)
                throw new ArgumentNullException(nameof(groupMatchRegex));

            SetHelpers<SigninCard, SigninCardAssertionFailedException>.TestWithGroups act =
                (SigninCard card, out IList<string> matches) => card.That().HasTextMatching(regex, groupMatchRegex, out matches);
            matchedGroups = _setHelpers.TestSetForMatchAndReturnGroups(SigninCards, act, CreateEx(nameof(SigninCard.Text), regex));

            return this;
        }

        public ICardActionAssertions WithButtonsThat()
        {
            return new CardActionSetAssertions(SigninCards);
        }

        public Func<SigninCardAssertionFailedException> CreateEx(string testedProperty, string regex)
        {
            var message = $"Expected at least one sigin card in set to have property {testedProperty} to match {regex} but none did.";
            return () => new SigninCardAssertionFailedException(message);
        }
    }
}