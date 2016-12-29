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
            var attachmentExtractor = new AttachmentExtractor();
            var thumbnailCards = attachmentExtractor.ExtractThumbnailCardsFromMessage(_message);

            return new ThumbnailCardSetAssertions(thumbnailCards);
        }

        public IHeroCardAssertions OfTypeHeroCardThat()
        {
            throw new System.NotImplementedException();
        }

        public ISigninCardAssertions OfTypeSigninCardThat()
        {
            throw new System.NotImplementedException();
        }

        public IReceiptCardAssertions OfTypeReceiptCardThat()
        {
            throw new System.NotImplementedException();
        }
    }
}