using KBrimble.DirectLineTester.Assertions.Cards;
using KBrimble.DirectLineTester.Attachments;
using KBrimble.DirectLineTester.Models.Cards;
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
            var attachmentExtractor = AttachmentExtractorFactory.GetAttachmentExtractor();
            var thumbnailCards = attachmentExtractor.ExtractCardsFromMessage<ThumbnailCard>(_message);

            return new ThumbnailCardSetAssertions(thumbnailCards);
        }

        public IHeroCardAssertions OfTypeHeroCardThat()
        {
            var attachmentExtractor = AttachmentExtractorFactory.GetAttachmentExtractor();
            var heroCards = attachmentExtractor.ExtractCardsFromMessage<HeroCard>(_message);

            return new HeroCardSetAssertions(heroCards);
        }

        public ISigninCardAssertions OfTypeSigninCardThat()
        {
            var attachmentExtractor = AttachmentExtractorFactory.GetAttachmentExtractor();
            var signinCards = attachmentExtractor.ExtractCardsFromMessage<SigninCard>(_message);

            return new SigninCardSetAssertions(signinCards);
        }

        public IReceiptCardAssertions OfTypeReceiptCardThat()
        {
            var attachmentExtractor = AttachmentExtractorFactory.GetAttachmentExtractor();
            var receiptCards = attachmentExtractor.ExtractCardsFromMessage<ReceiptCard>(_message);

            return new ReceiptCardSetAssertions(receiptCards);
        }
    }
}