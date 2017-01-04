using System.Collections.Generic;
using Microsoft.Bot.Connector.DirectLine.Models;

namespace Expecto.Attachments
{
    public interface IAttachmentExtractor
    {
        IEnumerable<T> ExtractCards<T>(MessageSet messageSet);
        IEnumerable<T> ExtractCards<T>(IEnumerable<Message> messageSet);
        IEnumerable<T> ExtractCards<T>(Message message);
    }
}