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
            return new ThumbnailCardSetAssertions(_message);
        }

        public IHeroCardAssertions OfTypeHeroCardThat()
        {
            return new HeroCardSetAssertions(_message);
        }

        public ISigninCardAssertions OfTypeSigninCardThat()
        {
            return new SigninCardSetAssertions(_message);
        }

        public IReceiptCardAssertions OfTypeReceiptCardThat()
        {
            return new ReceiptCardSetAssertions(_message);
        }
    }
}