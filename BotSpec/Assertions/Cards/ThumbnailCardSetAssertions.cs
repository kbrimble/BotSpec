using System;
using System.Collections.Generic;
using System.Linq;
using BotSpec.Assertions.Cards.CardComponents;
using BotSpec.Attachments;
using BotSpec.Exceptions;
using Microsoft.Bot.Connector.DirectLine;

namespace BotSpec.Assertions.Cards
{
    internal class ThumbnailCardSetAssertions : IThumbnailCardAssertions, IThrow<ThumbnailCardAssertionFailedException>
    {
        public readonly IEnumerable<ThumbnailCard> ThumbnailCards;
        private readonly SetHelpers<ThumbnailCard, ThumbnailCardAssertionFailedException> _setHelpers;

        public ThumbnailCardSetAssertions(ActivitySet activitySet) : this()
        {
            var attachmentExtractor = AttachmentExtractorFactory.GetAttachmentExtractor();
            ThumbnailCards = attachmentExtractor.ExtractCards<ThumbnailCard>(activitySet);
        }

        public ThumbnailCardSetAssertions(IEnumerable<Activity> activitySet) : this()
        {
            var attachmentExtractor = AttachmentExtractorFactory.GetAttachmentExtractor();
            ThumbnailCards = attachmentExtractor.ExtractCards<ThumbnailCard>(activitySet);
        }

        public ThumbnailCardSetAssertions(Activity activity) : this()
        {
            var attachmentExtractor = AttachmentExtractorFactory.GetAttachmentExtractor();
            ThumbnailCards = attachmentExtractor.ExtractCards<ThumbnailCard>(activity);
        }

        public ThumbnailCardSetAssertions(IEnumerable<ThumbnailCard> thumbnailCards) : this()
        {
            ThumbnailCards = thumbnailCards;
        }

        private ThumbnailCardSetAssertions()
        {
            _setHelpers = new SetHelpers<ThumbnailCard, ThumbnailCardAssertionFailedException>();
        }

        public IThumbnailCardAssertions SubtitleMatching(string regex)
        {
            if (regex == null)
                throw new ArgumentNullException(nameof(regex));

            _setHelpers.TestSetForMatch(ThumbnailCards, card => card.That().SubtitleMatching(regex), CreateEx(nameof(ThumbnailCard.Subtitle), regex));

            return this;
        }

        public IThumbnailCardAssertions SubtitleMatching(string regex, string groupMatchRegex, out IList<string> matchedGroups)
        {
            if (regex == null)
                throw new ArgumentNullException(nameof(regex));
            if (groupMatchRegex == null)
                throw new ArgumentNullException(nameof(groupMatchRegex));

            SetHelpers<ThumbnailCard, ThumbnailCardAssertionFailedException>.TestWithGroups act
                = (ThumbnailCard card, out IList<string> matches) => card.That().SubtitleMatching(regex, groupMatchRegex, out matches);
            matchedGroups = _setHelpers.TestSetForMatchAndReturnGroups(ThumbnailCards, act, CreateEx(nameof(ThumbnailCard.Subtitle), regex));

            return this;
        }

        public IThumbnailCardAssertions TextMatching(string regex)
        {
            if (regex == null)
                throw new ArgumentNullException(nameof(regex));

            _setHelpers.TestSetForMatch(ThumbnailCards, card => card.That().TextMatching(regex), CreateEx(nameof(ThumbnailCard.Text), regex));

            return this;
        }

        public IThumbnailCardAssertions TextMatching(string regex, string groupMatchRegex, out IList<string> matchedGroups)
        {
            if (regex == null)
                throw new ArgumentNullException(nameof(regex));
            if (groupMatchRegex == null)
                throw new ArgumentNullException(nameof(groupMatchRegex));

            SetHelpers<ThumbnailCard, ThumbnailCardAssertionFailedException>.TestWithGroups act
                = (ThumbnailCard card, out IList<string> matches) => card.That().TextMatching(regex, groupMatchRegex, out matches);
            matchedGroups = _setHelpers.TestSetForMatchAndReturnGroups(ThumbnailCards, act, CreateEx(nameof(ThumbnailCard.Text), regex));

            return this;
        }

        public IThumbnailCardAssertions TitleMatching(string regex)
        {
            if (regex == null)
                throw new ArgumentNullException(nameof(regex));

            _setHelpers.TestSetForMatch(ThumbnailCards, card => card.That().TitleMatching(regex), CreateEx(nameof(ThumbnailCard.Title), regex));

            return this;
        }

        public IThumbnailCardAssertions TitleMatching(string regex, string groupMatchRegex, out IList<string> matchedGroups)
        {
            if (regex == null)
                throw new ArgumentNullException(nameof(regex));
            if (groupMatchRegex == null)
                throw new ArgumentNullException(nameof(groupMatchRegex));

            SetHelpers<ThumbnailCard, ThumbnailCardAssertionFailedException>.TestWithGroups act
                = (ThumbnailCard card, out IList<string> matches) => card.That().TitleMatching(regex, groupMatchRegex, out matches);
            matchedGroups = _setHelpers.TestSetForMatchAndReturnGroups(ThumbnailCards, act, CreateEx(nameof(ThumbnailCard.Title), regex));

            return this;
        }

        public Func<ThumbnailCardAssertionFailedException> CreateEx(string testedProperty, string regex)
        {
            var activity = $"Expected at least one thumbnail card in set to have property {testedProperty} to match {regex} but none did.";
            return () => new ThumbnailCardAssertionFailedException(activity);
        }

        public ICardImageAssertions WithCardImage()
        {
            var images = ThumbnailCards.Where(card => card.Images != null && card.Images.Any()).SelectMany(card => card.Images).ToList();
            return new CardImageSetAssertions(images);
        }

        public ICardActionAssertions WithButtons()
        {
            var buttons = ThumbnailCards.Where(card => card.Buttons != null && card.Buttons.Any()).SelectMany(card => card.Buttons).ToList();
            return new CardActionSetAssertions(buttons);
        }

        public ICardActionAssertions WithTapAction()
        {
            var tapActions = ThumbnailCards.Select(card => card.Tap).Where(tap => tap != null).ToList();
            return new CardActionSetAssertions(tapActions);
        }
    }
}
