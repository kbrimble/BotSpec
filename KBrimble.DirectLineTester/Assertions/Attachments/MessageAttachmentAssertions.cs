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

        public IThumbnailCardAssertions OfTypeThumbnailCard()
        {
            return new ThumbnailCardSetAssertions(_message);
        }

        public IHeroCardAssertions OfTypeHeroCard()
        {
            return new HeroCardSetAssertions(_message);
        }

        public ISigninCardAssertions OfTypeSigninCard()
        {
            return new SigninCardSetAssertions(_message);
        }

        public IReceiptCardAssertions OfTypeReceiptCard()
        {
            return new ReceiptCardSetAssertions(_message);
        }
    }
}