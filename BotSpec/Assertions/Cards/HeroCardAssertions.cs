using System;
using System.Collections.Generic;
using BotSpec.Assertions.Cards.CardComponents;
using BotSpec.Exceptions;
using BotSpec.Models.Cards;

namespace BotSpec.Assertions.Cards
{
    internal class HeroCardAssertions : IHeroCardAssertions, IThrow<HeroCardAssertionFailedException>
    {
        private readonly HeroCard _heroCard;
        private readonly StringHelpers<HeroCardAssertionFailedException> _stringHelpers;

        public HeroCardAssertions(HeroCard heroCard)
        {
            _heroCard = heroCard;
            _stringHelpers = new StringHelpers<HeroCardAssertionFailedException>();
        }

        public IHeroCardAssertions TextMatching(string regex)
        {
            if (regex == null)
                throw new ArgumentNullException(nameof(regex));

            _stringHelpers.TestForMatch(_heroCard.Text, regex, CreateEx(nameof(_heroCard.Text), regex));

            return this;
        }

        public IHeroCardAssertions TextMatching(string regex, string groupMatchRegex, out IList<string> matchedGroups)
        {
            if (regex == null)
                throw new ArgumentNullException(nameof(regex));
            if (groupMatchRegex == null)
                throw new ArgumentNullException(nameof(groupMatchRegex));

            matchedGroups = _stringHelpers.TestForMatchAndReturnGroups(_heroCard.Text, regex, groupMatchRegex, CreateEx(nameof(_heroCard.Text), regex));

            return this;
        }

        public IHeroCardAssertions TitleMatching(string regex)
        {
            if (regex == null)
                throw new ArgumentNullException(nameof(regex));

            _stringHelpers.TestForMatch(_heroCard.Title, regex, CreateEx(nameof(_heroCard.Title), regex));

            return this;
        }

        public IHeroCardAssertions TitleMatching(string regex, string groupMatchRegex, out IList<string> matchedGroups)
        {
            if (regex == null)
                throw new ArgumentNullException(nameof(regex));
            if (groupMatchRegex == null)
                throw new ArgumentNullException(nameof(groupMatchRegex));

            matchedGroups = _stringHelpers.TestForMatchAndReturnGroups(_heroCard.Title, regex, groupMatchRegex, CreateEx(nameof(_heroCard.Title), regex));

            return this;
        }

        public IHeroCardAssertions SubtitleMatching(string regex)
        {
            if (regex == null)
                throw new ArgumentNullException(nameof(regex));

            _stringHelpers.TestForMatch(_heroCard.Subtitle, regex, CreateEx(nameof(_heroCard.Subtitle), regex));

            return this;
        }

        public IHeroCardAssertions SubtitleMatching(string regex, string groupMatchRegex, out IList<string> matchedGroups)
        {
            if (regex == null)
                throw new ArgumentNullException(nameof(regex));
            if (groupMatchRegex == null)
                throw new ArgumentNullException(nameof(groupMatchRegex));

            matchedGroups = _stringHelpers.TestForMatchAndReturnGroups(_heroCard.Subtitle, regex, groupMatchRegex, CreateEx(nameof(_heroCard.Subtitle), regex));

            return this;
        }

        public ICardActionAssertions WithButtons()
        {
            return new CardActionSetAssertions(_heroCard.Buttons);
        }

        public ICardImageAssertions WithCardImage()
        {
            return new CardImageSetAssertions(_heroCard.Images);
        }

        public ICardActionAssertions WithTapAction()
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