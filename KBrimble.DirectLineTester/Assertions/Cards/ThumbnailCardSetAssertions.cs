using System;
using System.Collections.Generic;
using KBrimble.DirectLineTester.Assertions.Cards.CardComponents;
using KBrimble.DirectLineTester.Attachments;
using KBrimble.DirectLineTester.Exceptions;
using KBrimble.DirectLineTester.Models.Cards;
using Microsoft.Bot.Connector.DirectLine.Models;

namespace KBrimble.DirectLineTester.Assertions.Cards
{
    public class ThumbnailCardSetAssertions : IThumbnailCardAssertions
    {
        public readonly IEnumerable<ThumbnailCard> ThumbnailCards;
        private readonly SetHelpers<ThumbnailCard, ThumbnailCardAssertionFailedException> _setHelpers;

        public ThumbnailCardSetAssertions(MessageSet messageSet) : this()
        {
            var attachmentExtractor = AttachmentExtractorFactory.GetAttachmentExtractor();
            ThumbnailCards = attachmentExtractor.ExtractCardsFromMessageSet<ThumbnailCard>(messageSet);
        }

        public ThumbnailCardSetAssertions(IEnumerable<Message> messageSet) : this()
        {
            var attachmentExtractor = AttachmentExtractorFactory.GetAttachmentExtractor();
            ThumbnailCards = attachmentExtractor.ExtractCardsFromMessageSet<ThumbnailCard>(messageSet);
        }

        public ThumbnailCardSetAssertions(Message message) : this()
        {
            var attachmentExtractor = AttachmentExtractorFactory.GetAttachmentExtractor();
            ThumbnailCards = attachmentExtractor.ExtractCardsFromMessage<ThumbnailCard>(message);
        }

        public ThumbnailCardSetAssertions(IEnumerable<ThumbnailCard> thumbnailCards) : this()
        {
            ThumbnailCards = thumbnailCards;
        }

        private ThumbnailCardSetAssertions()
        {
            _setHelpers = new SetHelpers<ThumbnailCard, ThumbnailCardAssertionFailedException>();
        }

        public IThumbnailCardAssertions HasSubtitleMatching(string regex)
        {
            if (regex == null)
                throw new ArgumentNullException(nameof(regex));

            _setHelpers.TestSetForMatch(ThumbnailCards, card => card.That().HasSubtitleMatching(regex), CreateEx(nameof(ThumbnailCard.Subtitle), regex));

            return this;
        }

        public IThumbnailCardAssertions HasSubtitleMatching(string regex, string groupMatchRegex, out IList<string> matchedGroups)
        {
            if (regex == null)
                throw new ArgumentNullException(nameof(regex));
            if (groupMatchRegex == null)
                throw new ArgumentNullException(nameof(groupMatchRegex));

            SetHelpers<ThumbnailCard, ThumbnailCardAssertionFailedException>.TestWithGroups act
                = (ThumbnailCard card, out IList<string> matches) => card.That().HasSubtitleMatching(regex, groupMatchRegex, out matches);
            matchedGroups = _setHelpers.TestSetForMatchAndReturnGroups(ThumbnailCards, act, CreateEx(nameof(ThumbnailCard.Subtitle), regex));

            return this;
        }

        public IThumbnailCardAssertions HasTextMatching(string regex)
        {
            if (regex == null)
                throw new ArgumentNullException(nameof(regex));

            _setHelpers.TestSetForMatch(ThumbnailCards, card => card.That().HasTextMatching(regex), CreateEx(nameof(ThumbnailCard.Text), regex));

            return this;
        }

        public IThumbnailCardAssertions HasTextMatching(string regex, string groupMatchRegex, out IList<string> matchedGroups)
        {
            if (regex == null)
                throw new ArgumentNullException(nameof(regex));
            if (groupMatchRegex == null)
                throw new ArgumentNullException(nameof(groupMatchRegex));

            SetHelpers<ThumbnailCard, ThumbnailCardAssertionFailedException>.TestWithGroups act
                = (ThumbnailCard card, out IList<string> matches) => card.That().HasTextMatching(regex, groupMatchRegex, out matches);
            matchedGroups = _setHelpers.TestSetForMatchAndReturnGroups(ThumbnailCards, act, CreateEx(nameof(ThumbnailCard.Text), regex));

            return this;
        }

        public IThumbnailCardAssertions HasTitleMatching(string regex)
        {
            if (regex == null)
                throw new ArgumentNullException(nameof(regex));

            _setHelpers.TestSetForMatch(ThumbnailCards, card => card.That().HasTitleMatching(regex), CreateEx(nameof(ThumbnailCard.Title), regex));

            return this;
        }

        public IThumbnailCardAssertions HasTitleMatching(string regex, string groupMatchRegex, out IList<string> matchedGroups)
        {
            if (regex == null)
                throw new ArgumentNullException(nameof(regex));
            if (groupMatchRegex == null)
                throw new ArgumentNullException(nameof(groupMatchRegex));

            SetHelpers<ThumbnailCard, ThumbnailCardAssertionFailedException>.TestWithGroups act
                = (ThumbnailCard card, out IList<string> matches) => card.That().HasTitleMatching(regex, groupMatchRegex, out matches);
            matchedGroups = _setHelpers.TestSetForMatchAndReturnGroups(ThumbnailCards, act, CreateEx(nameof(ThumbnailCard.Title), regex));

            return this;
        }

        public Func<ThumbnailCardAssertionFailedException> CreateEx(string testedProperty, string regex)
        {
            var message = $"Expected at least one thumbnail card in set to have property {testedProperty} to match {regex} but none did.";
            return () => new ThumbnailCardAssertionFailedException(message);
        }

        public ICardImageAssertions WithCardImageThat()
        {
            return new CardImageSetAssertions(ThumbnailCards);
        }

        public ICardActionAssertions WithButtonsThat()
        {
            return new CardActionSetAssertions(ThumbnailCards as IEnumerable<IHaveButtons>);
        }

        public ICardActionAssertions WithTapActionThat()
        {
            return new CardActionSetAssertions(ThumbnailCards as IEnumerable<IHaveTapAction>);
        }
    }
}
