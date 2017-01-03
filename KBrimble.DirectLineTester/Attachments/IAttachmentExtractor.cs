using System.Collections.Generic;
using KBrimble.DirectLineTester.Models.Cards;
using Microsoft.Bot.Connector.DirectLine.Models;

namespace KBrimble.DirectLineTester.Attachments
{
    public interface IAttachmentExtractor
    {
        IEnumerable<T> ExtractCards<T>(MessageSet messageSet);
        IEnumerable<T> ExtractCards<T>(IEnumerable<Message> messageSet);
        IEnumerable<T> ExtractCards<T>(Message message);
    }
}