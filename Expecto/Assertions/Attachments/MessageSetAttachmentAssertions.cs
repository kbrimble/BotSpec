using System.Collections.Generic;
using Expecto.Assertions.Cards;
using Microsoft.Bot.Connector.DirectLine.Models;

namespace Expecto.Assertions.Attachments
{
    internal class MessageSetAttachmentAssertions : IMessageAttachmentAssertions
    {
        private readonly IEnumerable<Message> _messageSet;

        public MessageSetAttachmentAssertions(IEnumerable<Message> messageSet)
        {
            _messageSet = messageSet;
        }

        public IThumbnailCardAssertions OfTypeThumbnailCard()
        {
            return new ThumbnailCardSetAssertions(_messageSet);
        }

        public IHeroCardAssertions OfTypeHeroCard()
        {
            return new HeroCardSetAssertions(_messageSet);
        }

        public ISigninCardAssertions OfTypeSigninCard()
        {
            return new SigninCardSetAssertions(_messageSet);
        }

        public IReceiptCardAssertions OfTypeReceiptCard()
        {
            return new ReceiptCardSetAssertions(_messageSet);
        }
    }
}