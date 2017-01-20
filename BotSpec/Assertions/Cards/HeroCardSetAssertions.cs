using System;
using System.Collections.Generic;
using System.Linq;
using BotSpec.Assertions.Cards.CardComponents;
using BotSpec.Attachments;
using BotSpec.Exceptions;
using Microsoft.Bot.Connector.DirectLine;

namespace BotSpec.Assertions.Cards
{
    internal class HeroCardSetAssertions : IHeroCardAssertions, IThrow<HeroCardAssertionFailedException>
    {
        public readonly IEnumerable<HeroCard> HeroCards;
        private readonly SetHelpers<HeroCard, HeroCardAssertionFailedException> _setHelpers;

        public HeroCardSetAssertions(IEnumerable<Activity> activitySet) : this()
        {
            var attachmentExtractor = AttachmentExtractorFactory.GetAttachmentExtractor();
            HeroCards = attachmentExtractor.ExtractCards<HeroCard>(activitySet);
        }

        public HeroCardSetAssertions(Activity activity) : this()
        {
            var attachmentExtractor = AttachmentExtractorFactory.GetAttachmentExtractor();
            HeroCards = attachmentExtractor.ExtractCards<HeroCard>(activity);
        }

        public HeroCardSetAssertions(IEnumerable<HeroCard> heroCards) : this()
        {
            HeroCards = heroCards;
        }

        private HeroCardSetAssertions()
        {
            _setHelpers = new SetHelpers<HeroCard, HeroCardAssertionFailedException>();
        }

        public IHeroCardAssertions SubtitleMatching(string regex)
        {
            if (regex == null)
                throw new ArgumentNullException(nameof(regex));

            _setHelpers.TestSetForMatch(HeroCards, card => card.That().SubtitleMatching(regex), CreateEx(nameof(HeroCard.Subtitle), regex));

            return this;
        }

        public IHeroCardAssertions SubtitleMatching(string regex, string groupMatchRegex, out IList<string> matchedGroups)
        {
            if (regex == null)
                throw new ArgumentNullException(nameof(regex));
            if (groupMatchRegex == null)
                throw new ArgumentNullException(nameof(groupMatchRegex));

            SetHelpers<HeroCard, HeroCardAssertionFailedException>.TestWithGroups act
                = (HeroCard card, out IList<string> matches) => card.That().SubtitleMatching(regex, groupMatchRegex, out matches);
            matchedGroups = _setHelpers.TestSetForMatchAndReturnGroups(HeroCards, act, CreateEx(nameof(HeroCard.Subtitle), regex));

            return this;
        }

        public IHeroCardAssertions TextMatching(string regex)
        {
            if (regex == null)
                throw new ArgumentNullException(nameof(regex));

            _setHelpers.TestSetForMatch(HeroCards, card => card.That().TextMatching(regex), CreateEx(nameof(HeroCard.Text), regex));

            return this;
        }

        public IHeroCardAssertions TextMatching(string regex, string groupMatchRegex, out IList<string> matchedGroups)
        {
            if (regex == null)
                throw new ArgumentNullException(nameof(regex));
            if (groupMatchRegex == null)
                throw new ArgumentNullException(nameof(groupMatchRegex));

            SetHelpers<HeroCard, HeroCardAssertionFailedException>.TestWithGroups act
                = (HeroCard card, out IList<string> matches) => card.That().TextMatching(regex, groupMatchRegex, out matches);
            matchedGroups = _setHelpers.TestSetForMatchAndReturnGroups(HeroCards, act, CreateEx(nameof(HeroCard.Text), regex));

            return this;
        }

        public IHeroCardAssertions TitleMatching(string regex)
        {
            if (regex == null)
                throw new ArgumentNullException(nameof(regex));

            _setHelpers.TestSetForMatch(HeroCards, card => card.That().TitleMatching(regex), CreateEx(nameof(HeroCard.Title), regex));

            return this;
        }

        public IHeroCardAssertions TitleMatching(string regex, string groupMatchRegex, out IList<string> matchedGroups)
        {
            if (regex == null)
                throw new ArgumentNullException(nameof(regex));
            if (groupMatchRegex == null)
                throw new ArgumentNullException(nameof(groupMatchRegex));

            SetHelpers<HeroCard, HeroCardAssertionFailedException>.TestWithGroups act
                = (HeroCard card, out IList<string> matches) => card.That().TitleMatching(regex, groupMatchRegex, out matches);
            matchedGroups = _setHelpers.TestSetForMatchAndReturnGroups(HeroCards, act, CreateEx(nameof(HeroCard.Title), regex));

            return this;
        }

        public Func<HeroCardAssertionFailedException> CreateEx(string testedProperty, string regex)
        {
            var activity = $"Expected at least one hero card in set to have property {testedProperty} to match {regex} but none did.";
            return () => new HeroCardAssertionFailedException(activity);
        }

        public ICardImageAssertions WithCardImage()
        {
            var images = HeroCards.Where(card => card.Images != null && card.Images.Any()).SelectMany(card => card.Images).ToList();
            return new CardImageSetAssertions(images);
        }

        public ICardActionAssertions WithButtons()
        {
            var buttons = HeroCards.Where(card => card.Buttons != null).SelectMany(card => card.Buttons).ToList();
            return new CardActionSetAssertions(buttons);
        }

        public ICardActionAssertions WithTapAction()
        {
            var tapActions = HeroCards.Select(card => card.Tap).Where(tap => tap != null).ToList();
            return new CardActionSetAssertions(tapActions);
        }
    }
}