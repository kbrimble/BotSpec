using System;
using System.Collections.Generic;
using System.Linq;
using KBrimble.DirectLineTester.Models.Cards;
using Microsoft.Bot.Connector.DirectLine.Models;
using Newtonsoft.Json;

namespace KBrimble.DirectLineTester
{
    public static class AttachmentExtractor
    {
        public static IList<ThumbnailCard> ExtractThumbnailCardsFromMessage(Message message)
        {
            try
            {
                var thumbnailCardAttachments = message.Attachments.Where(att => att.ContentType == ThumbnailCard.ContentType);
                return thumbnailCardAttachments.Select(tc => JsonConvert.DeserializeObject<ThumbnailCard>(tc.ToString())).ToList();
            }
            catch (Exception)
            {
                return new List<ThumbnailCard>();
            }
        }
    }
}