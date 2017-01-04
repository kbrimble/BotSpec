using Expecto.Assertions.Cards;
using Microsoft.Bot.Connector.DirectLine.Models;

namespace Expecto.Assertions.Attachments
{
    internal class MessageAttachmentAssertions : IMessageAttachmentAssertions
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