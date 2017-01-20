using System.Collections.Generic;
using Microsoft.Bot.Connector.DirectLine;

namespace BotSpec.Attachments
{
    public interface IAttachmentExtractor
    {
        IEnumerable<T> ExtractCards<T>(ActivitySet activitySet);
        IEnumerable<T> ExtractCards<T>(IEnumerable<Activity> activitySet);
        IEnumerable<T> ExtractCards<T>(Activity activity);
    }
}