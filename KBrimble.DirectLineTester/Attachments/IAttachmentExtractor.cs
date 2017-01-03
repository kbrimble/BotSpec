using System.Collections.Generic;
using KBrimble.DirectLineTester.Models.Cards;
using Microsoft.Bot.Connector.DirectLine.Models;

namespace KBrimble.DirectLineTester.Attachments
{
    public interface IAttachmentExtractor
    {
        IEnumerable<T> ExtractCardsFromMessageSet<T>(MessageSet messageSet);
        IEnumerable<T> ExtractCardsFromMessageSet<T>(IEnumerable<Message> messageSet);
        IEnumerable<T> ExtractCardsFromMessage<T>(Message message);
    }
}