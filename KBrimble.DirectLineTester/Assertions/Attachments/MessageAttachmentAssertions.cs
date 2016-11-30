using KBrimble.DirectLineTester.Assertions.Cards;
using KBrimble.DirectLineTester.Attachments;
using Microsoft.Bot.Connector.DirectLine.Models;

namespace KBrimble.DirectLineTester.Assertions.Attachments
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
            var attachmentExtractor = new AttachmentExtractor(AttachmentRetreiverFactory.DefaultAttachmentRetreiver());
            var thumbnailCards = attachmentExtractor.ExtractThumbnailCardsFromMessage(_message);

            return new ThumbnailCardSetAssertions(thumbnailCards);
        }
    }
}