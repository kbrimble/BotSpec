using System;
using System.Collections.Generic;
using KBrimble.DirectLineTester.Attachments;
using KBrimble.DirectLineTester.Exceptions;
using KBrimble.DirectLineTester.Models.Cards;
using Microsoft.Bot.Connector.DirectLine.Models;

namespace KBrimble.DirectLineTester.Assertions.Cards
{
    public class ThumbnailCardSetAssertions : IThumbnailCardAssertions, IThrow<ThumbnailCardSetAssertionFailedException>
    {
        private readonly IEnumerable<ThumbnailCard> _thumbnailCards;
        private readonly SetHelpers<ThumbnailCard, ThumbnailCardAssertionFailedException, ThumbnailCardSetAssertionFailedException> _setHelpers;

        public ThumbnailCardSetAssertions(Message message) : this()
        {
            var attachmentExtractor = new AttachmentExtractor(AttachmentRetreiverFactory.DefaultAttachmentRetreiver());
            _thumbnailCards = attachmentExtractor.ExtractThumbnailCardsFromMessage(message);
        }

        public ThumbnailCardSetAssertions(IEnumerable<ThumbnailCard> thumbnailCards) : this()
        {
            _thumbnailCards = thumbnailCards;
        }

        private ThumbnailCardSetAssertions()
        {
            _setHelpers = new SetHelpers<ThumbnailCard, ThumbnailCardAssertionFailedException, ThumbnailCardSetAssertionFailedException>();
        }

        public IThumbnailCardAssertions HasSubtitleMatching(string regex)
        {
            if (regex == null)
                throw new ArgumentNullException(nameof(regex));
            
            _setHelpers.TestSetForMatch(_thumbnailCards, card => card.That().HasSubtitleMatching(regex), CreateEx(nameof(ThumbnailCard.Subtitle), regex));

            return this;
        }

        public IThumbnailCardAssertions HasSubtitleMatching(string regex, string groupMatchRegex, out IList<string> matchedGroups)
        {
            if (regex == null)
                throw new ArgumentNullException(nameof(regex));
            if (groupMatchRegex == null)
                throw new ArgumentNullException(nameof(groupMatchRegex));

            SetHelpers<ThumbnailCard, ThumbnailCardAssertionFailedException, ThumbnailCardSetAssertionFailedException>.TestWithGroups act
                = (ThumbnailCard card, out IList<string> matches) => card.That().HasSubtitleMatching(regex, groupMatchRegex, out matches);
            matchedGroups = _setHelpers.TestSetForMatchAndReturnGroups(_thumbnailCards, act, CreateEx(nameof(ThumbnailCard.Subtitle), regex));

            return this;
        }

        public IThumbnailCardAssertions HasTextMatching(string regex)
        {
            if (regex == null)
                throw new ArgumentNullException(nameof(regex));

            _setHelpers.TestSetForMatch(_thumbnailCards, card => card.That().HasTextMatching(regex), CreateEx(nameof(ThumbnailCard.Text), regex));

            return this;
        }

        public IThumbnailCardAssertions HasTextMatching(string regex, string groupMatchRegex, out IList<string> matchedGroups)
        {
            if (regex == null)
                throw new ArgumentNullException(nameof(regex));
            if (groupMatchRegex == null)
                throw new ArgumentNullException(nameof(groupMatchRegex));

            SetHelpers<ThumbnailCard, ThumbnailCardAssertionFailedException, ThumbnailCardSetAssertionFailedException>.TestWithGroups act
                = (ThumbnailCard card, out IList<string> matches) => card.That().HasTextMatching(regex, groupMatchRegex, out matches);
            matchedGroups = _setHelpers.TestSetForMatchAndReturnGroups(_thumbnailCards, act, CreateEx(nameof(ThumbnailCard.Text), regex));

            return this;
        }

        public IThumbnailCardAssertions HasTitleMatching(string regex)
        {
            if (regex == null)
                throw new ArgumentNullException(nameof(regex));

            _setHelpers.TestSetForMatch(_thumbnailCards, card => card.That().HasTitleMatching(regex), CreateEx(nameof(ThumbnailCard.Title), regex));

            return this;
        }

        public IThumbnailCardAssertions HasTitleMatching(string regex, string groupMatchRegex, out IList<string> matchedGroups)
        {
            if (regex == null)
                throw new ArgumentNullException(nameof(regex));
            if (groupMatchRegex == null)
                throw new ArgumentNullException(nameof(groupMatchRegex));

            SetHelpers<ThumbnailCard, ThumbnailCardAssertionFailedException, ThumbnailCardSetAssertionFailedException>.TestWithGroups act
                = (ThumbnailCard card, out IList<string> matches) => card.That().HasTitleMatching(regex, groupMatchRegex, out matches);
            matchedGroups = _setHelpers.TestSetForMatchAndReturnGroups(_thumbnailCards, act, CreateEx(nameof(ThumbnailCard.Title), regex));

            return this;
        }

        public ThumbnailCardSetAssertionFailedException CreateEx(string testedProperty, string regex)
        {
            var message = $"Expected at least one thumbnail card in set to have property {testedProperty} to match {regex} but none did.";
            return new ThumbnailCardSetAssertionFailedException(message);
        }

        public ICardImageAssertions WithCardImageThat()
        {
            throw new NotImplementedException();
        }
    }
}
