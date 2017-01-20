using System;
using System.Collections.Generic;
using System.Linq;
using BotSpec.Assertions.Cards.CardComponents;
using BotSpec.Attachments;
using BotSpec.Exceptions;
using Microsoft.Bot.Connector.DirectLine;

namespace BotSpec.Assertions.Cards
{
    internal class SigninCardSetAssertions : ISigninCardAssertions, IThrow<SigninCardAssertionFailedException>
    {
        public readonly IEnumerable<SigninCard> SigninCards;
        private readonly SetHelpers<SigninCard, SigninCardAssertionFailedException> _setHelpers;

        public SigninCardSetAssertions(Activity activity) : this()
        {
            var attachmentExtractor = AttachmentExtractorFactory.GetAttachmentExtractor();
            SigninCards = attachmentExtractor.ExtractCards<SigninCard>(activity);
        }

        public SigninCardSetAssertions(IEnumerable<Activity> activitySet) : this()
        {
            var attachmentExtractor = AttachmentExtractorFactory.GetAttachmentExtractor();
            SigninCards = attachmentExtractor.ExtractCards<SigninCard>(activitySet);
        }

        public SigninCardSetAssertions(IEnumerable<SigninCard> signinCards) : this()
        {
            SigninCards = signinCards;
        }

        private SigninCardSetAssertions()
        {
            _setHelpers = new SetHelpers<SigninCard, SigninCardAssertionFailedException>();
        }

        public ISigninCardAssertions TextMatching(string regex)
        {
            if (regex == null)
                throw new ArgumentNullException(nameof(regex));

            _setHelpers.TestSetForMatch(SigninCards, card => card.That().TextMatching(regex), CreateEx(nameof(SigninCard.Text), regex));

            return this;
        }

        public ISigninCardAssertions TextMatching(string regex, string groupMatchRegex, out IList<string> matchedGroups)
        {
            if (regex == null)
                throw new ArgumentNullException(nameof(regex));
            if (groupMatchRegex == null)
                throw new ArgumentNullException(nameof(groupMatchRegex));

            SetHelpers<SigninCard, SigninCardAssertionFailedException>.TestWithGroups act =
                (SigninCard card, out IList<string> matches) => card.That().TextMatching(regex, groupMatchRegex, out matches);
            matchedGroups = _setHelpers.TestSetForMatchAndReturnGroups(SigninCards, act, CreateEx(nameof(SigninCard.Text), regex));

            return this;
        }

        public ICardActionAssertions WithButtons()
        {
            var buttons = SigninCards.Where(card => card.Buttons != null && card.Buttons.Any()).SelectMany(card => card.Buttons).ToList();
            return new CardActionSetAssertions(buttons);
        }

        public Func<SigninCardAssertionFailedException> CreateEx(string testedProperty, string regex)
        {
            var activity = $"Expected at least one sigin card in set to have property {testedProperty} to match {regex} but none did.";
            return () => new SigninCardAssertionFailedException(activity);
        }
    }
}