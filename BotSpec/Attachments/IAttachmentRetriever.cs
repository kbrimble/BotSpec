using System.Collections.Generic;

namespace BotSpec.Attachments
{
    public interface IAttachmentRetriever
    {
        string GetAttachmentFromUrl(string url);
        IEnumerable<string> GetAttachmentsFromUrls(params string[] urls);
    }
}