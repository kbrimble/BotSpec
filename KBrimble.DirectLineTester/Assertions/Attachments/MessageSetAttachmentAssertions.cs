using System.Collections.Generic;
using KBrimble.DirectLineTester.Assertions.Cards;
using Microsoft.Bot.Connector.DirectLine.Models;

namespace KBrimble.DirectLineTester.Assertions.Attachments
{
    public class MessageSetAttachmentAssertions : IMessageAttachmentAssertions
    {
        private readonly IEnumerable<Message> _messageSet;

        public MessageSetAttachmentAssertions(MessageSet messageSet)
        {
            _messageSet = messageSet.Messages;
        }

        public MessageSetAttachmentAssertions(IEnumerable<Message> messageSet)
        {
            _messageSet = messageSet;
        }

        public IThumbnailCardAssertions OfTypeThumbnailCardThat()
        {
            return new ThumbnailCardSetAssertions(_messageSet);
        }
    }
}