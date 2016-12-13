using System;
using System.Collections.Generic;
using KBrimble.DirectLineTester.Assertions.Cards.CardComponents;
using KBrimble.DirectLineTester.Exceptions;
using KBrimble.DirectLineTester.Models.Cards;

namespace KBrimble.DirectLineTester.Assertions.Cards
{
    public class HeroCardAssertions : IHeroCardAssertions, IThrow<HeroCardAssertionFailedException>
    {
        private readonly HeroCard _heroCard;

        public HeroCardAssertions(HeroCard heroCard)
        {
            _heroCard = heroCard;
        }

        public IHeroCardAssertions HasTextMatching(string regex)
        {
            if (regex == null)
                throw new ArgumentNullException(nameof(regex));

            StringHelpers.TestForMatch(_heroCard.Text, regex, CreateEx(nameof(_heroCard.Text), regex));

            return this;
        }

        public IHeroCardAssertions HasTextMatching(string regex, string groupMatchRegex, out IList<string> matchedGroups)
        {
            if (regex == null)
                throw new ArgumentNullException(nameof(regex));
            if (groupMatchRegex == null)
                throw new ArgumentNullException(nameof(groupMatchRegex));

            matchedGroups = StringHelpers.TestForMatchAndReturnGroups(_heroCard.Text, regex, groupMatchRegex, CreateEx(nameof(_heroCard.Text), regex));

            return this;
        }

        public IHeroCardAssertions HasTitleMatching(string regex)
        {
            if (regex == null)
                throw new ArgumentNullException(nameof(regex));

            StringHelpers.TestForMatch(_heroCard.Title, regex, CreateEx(nameof(_heroCard.Title), regex));

            return this;
        }

        public IHeroCardAssertions HasTitleMatching(string regex, string groupMatchRegex, out IList<string> matchedGroups)
        {
            if (regex == null)
                throw new ArgumentNullException(nameof(regex));
            if (groupMatchRegex == null)
                throw new ArgumentNullException(nameof(groupMatchRegex));

            matchedGroups = StringHelpers.TestForMatchAndReturnGroups(_heroCard.Title, regex, groupMatchRegex, CreateEx(nameof(_heroCard.Title), regex));

            return this;
        }

        public IHeroCardAssertions HasSubtitleMatching(string regex)
        {
            if (regex == null)
                throw new ArgumentNullException(nameof(regex));

            StringHelpers.TestForMatch(_heroCard.Subtitle, regex, CreateEx(nameof(_heroCard.Subtitle), regex));

            return this;
        }

        public IHeroCardAssertions HasSubtitleMatching(string regex, string groupMatchRegex, out IList<string> matchedGroups)
        {
            if (regex == null)
                throw new ArgumentNullException(nameof(regex));
            if (groupMatchRegex == null)
                throw new ArgumentNullException(nameof(groupMatchRegex));

            matchedGroups = StringHelpers.TestForMatchAndReturnGroups(_heroCard.Subtitle, regex, groupMatchRegex, CreateEx(nameof(_heroCard.Subtitle), regex));

            return this;
        }

        public ICardActionAssertions WithButtonsThat()
        {
            throw new System.NotImplementedException();
        }

        public ICardImageAssertions WithCardImageThat()
        {
            throw new System.NotImplementedException();
        }

        public ICardActionAssertions WithTapActionThat()
        {
            throw new System.NotImplementedException();
        }

        public HeroCardAssertionFailedException CreateEx(string testedProperty, string regex)
        {
            var message = $"Expected hero card to have property {testedProperty} to match {regex} but regex test failed.";
            return new HeroCardAssertionFailedException(message);
        }
    }
}