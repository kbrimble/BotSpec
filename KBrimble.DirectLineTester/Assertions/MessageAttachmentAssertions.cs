using KBrimble.DirectLineTester.Assertions.Cards;
using Microsoft.Bot.Connector.DirectLine.Models;

namespace KBrimble.DirectLineTester.Assertions
{
    public class MessageAttachmentAssertions : IMessageAttachmentAssertions
    {
        private readonly Message _message;

        public MessageAttachmentAssertions(Message message)
        {
            _message = message;
        }

        public IThumbnailCardAssertions OfTypeThumbnailCardThat()
        {
            var thumbnailCards = AttachmentExtractor.ExtractThumbnailCardsFromMessage(_message);

            return new ThumbnailCardSetAssertions(thumbnailCards);
        }
    }
}