using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Bot.Connector.DirectLine.Models;
using Newtonsoft.Json;

namespace KBrimble.DirectLineTester.Assertions
{

    public interface IMessageAttachmentAssertions
    {
        IThumbnailCardAssertions OfTypeThumbnailCardThat();
    }

    public class MessageAttachementAssertions : IMessageAttachmentAssertions
    {
        readonly Message message;

        public MessageAttachementAssertions(Message message)
        {
            this.message = message;
        }

        public IThumbnailCardAssertions OfTypeThumbnailCardThat()
        {
            var thumbnailCardAttachments = message.Attachments.Where(att => att.ContentType == ThumbnailCard.ContentType);
            var thumbnailCards = thumbnailCardAttachments.Select(tc => JsonConvert.DeserializeObject<ThumbnailCard>(tc.ToString()));

            return new ThumbnailCardSetAssertions(thumbnailCards);
        }
    }

    public class MessageSetAttachmentAssertions : IMessageAttachmentAssertions
    {
        readonly MessageSet messageSet;

        public MessageSetAttachmentAssertions(MessageSet messageSet)
        {
            this.messageSet = messageSet;
        }

        public IThumbnailCardAssertions OfTypeThumbnailCardThat()
        {
            throw new NotImplementedException();
        }
    }
}
