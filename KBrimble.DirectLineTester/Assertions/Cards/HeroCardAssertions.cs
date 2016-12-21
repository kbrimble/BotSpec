using System;
using System.Collections.Generic;
using KBrimble.DirectLineTester.Assertions.Cards.CardComponents;
using KBrimble.DirectLineTester.Exceptions;
using KBrimble.DirectLineTester.Models.Cards;

namespace KBrimble.DirectLineTester.Assertions.Cards
{
    public class HeroCardAssertions : IHeroCardAssertions
    {
        private readonly HeroCard _heroCard;
        private readonly StringHelpers<HeroCardAssertionFailedException> _stringHelpers;

        public HeroCardAssertions(HeroCard heroCard)
        {
            _heroCard = heroCard;
            _stringHelpers = new StringHelpers<HeroCardAssertionFailedException>();
        }

        public IHeroCardAssertions HasTextMatching(string regex)
        {
            if (regex == null)
                throw new ArgumentNullException(nameof(regex));

            _stringHelpers.TestForMatch(_heroCard.Text, regex, CreateEx(nameof(_heroCard.Text), regex));

            return this;
        }

        public IHeroCardAssertions HasTextMatching(string regex, string groupMatchRegex, out IList<string> matchedGroups)
        {
            if (regex == null)
                throw new ArgumentNullException(nameof(regex));
            if (groupMatchRegex == null)
                throw new ArgumentNullException(nameof(groupMatchRegex));

            matchedGroups = _stringHelpers.TestForMatchAndReturnGroups(_heroCard.Text, regex, groupMatchRegex, CreateEx(nameof(_heroCard.Text), regex));

            return this;
        }

        public IHeroCardAssertions HasTitleMatching(string regex)
        {
            if (regex == null)
                throw new ArgumentNullException(nameof(regex));

            _stringHelpers.TestForMatch(_heroCard.Title, regex, CreateEx(nameof(_heroCard.Title), regex));

            return this;
        }

        public IHeroCardAssertions HasTitleMatching(string regex, string groupMatchRegex, out IList<string> matchedGroups)
        {
            if (regex == null)
                throw new ArgumentNullException(nameof(regex));
            if (groupMatchRegex == null)
                throw new ArgumentNullException(nameof(groupMatchRegex));

            matchedGroups = _stringHelpers.TestForMatchAndReturnGroups(_heroCard.Title, regex, groupMatchRegex, CreateEx(nameof(_heroCard.Title), regex));

            return this;
        }

        public IHeroCardAssertions HasSubtitleMatching(string regex)
        {
            if (regex == null)
                throw new ArgumentNullException(nameof(regex));

            _stringHelpers.TestForMatch(_heroCard.Subtitle, regex, CreateEx(nameof(_heroCard.Subtitle), regex));

            return this;
        }

        public IHeroCardAssertions HasSubtitleMatching(string regex, string groupMatchRegex, out IList<string> matchedGroups)
        {
            if (regex == null)
                throw new ArgumentNullException(nameof(regex));
            if (groupMatchRegex == null)
                throw new ArgumentNullException(nameof(groupMatchRegex));

            matchedGroups = _stringHelpers.TestForMatchAndReturnGroups(_heroCard.Subtitle, regex, groupMatchRegex, CreateEx(nameof(_heroCard.Subtitle), regex));

            return this;
        }

        public ICardActionAssertions WithButtonsThat()
        {
            return new CardActionSetAssertions(_heroCard.Buttons);
        }

        public ICardImageAssertions WithCardImageThat()
        {
            return new CardImageSetAssertions(_heroCard.Images);
        }

        public ICardActionAssertions WithTapActionThat()
        {
            return new CardActionAssertions(_heroCard.Tap);
        }

        public Func<HeroCardAssertionFailedException> CreateEx(string testedProperty, string regex)
        {
            var message = $"Expected hero card to have property {testedProperty} to match {regex} but regex test failed.";
            return () => new HeroCardAssertionFailedException(message);
        }
    }
}