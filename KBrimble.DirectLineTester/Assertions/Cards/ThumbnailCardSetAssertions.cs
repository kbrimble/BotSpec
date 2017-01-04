using System;
using System.Collections.Generic;
using KBrimble.DirectLineTester.Assertions.Cards.CardComponents;
using KBrimble.DirectLineTester.Attachments;
using KBrimble.DirectLineTester.Exceptions;
using KBrimble.DirectLineTester.Models.Cards;
using Microsoft.Bot.Connector.DirectLine.Models;

namespace KBrimble.DirectLineTester.Assertions.Cards
{
    internal class ThumbnailCardSetAssertions : IThumbnailCardAssertions, IThrow<ThumbnailCardAssertionFailedException>
    {
        public readonly IEnumerable<ThumbnailCard> ThumbnailCards;
        private readonly SetHelpers<ThumbnailCard, ThumbnailCardAssertionFailedException> _setHelpers;

        public ThumbnailCardSetAssertions(MessageSet messageSet) : this()
        {
            var attachmentExtractor = AttachmentExtractorFactory.GetAttachmentExtractor();
            ThumbnailCards = attachmentExtractor.ExtractCards<ThumbnailCard>(messageSet);
        }

        public ThumbnailCardSetAssertions(IEnumerable<Message> messageSet) : this()
        {
            var attachmentExtractor = AttachmentExtractorFactory.GetAttachmentExtractor();
            ThumbnailCards = attachmentExtractor.ExtractCards<ThumbnailCard>(messageSet);
        }

        public ThumbnailCardSetAssertions(Message message) : this()
        {
            var attachmentExtractor = AttachmentExtractorFactory.GetAttachmentExtractor();
            ThumbnailCards = attachmentExtractor.ExtractCards<ThumbnailCard>(message);
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
            var message = $"Expected at least one thumbnail card in set to have property {testedProperty} to match {regex} but none did.";
            return () => new ThumbnailCardAssertionFailedException(message);
        }

        public ICardImageAssertions WithCardImage()
        {
            return new CardImageSetAssertions(ThumbnailCards);
        }

        public ICardActionAssertions WithButtons()
        {
            return new CardActionSetAssertions(ThumbnailCards as IEnumerable<IHaveButtons>);
        }

        public ICardActionAssertions WithTapAction()
        {
            return new CardActionSetAssertions(ThumbnailCards as IEnumerable<IHaveTapAction>);
        }
    }
}
