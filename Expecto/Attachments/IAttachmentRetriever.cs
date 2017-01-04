using System.Collections.Generic;

namespace Expecto.Attachments
{
    public interface IAttachmentRetriever
    {
        string GetAttachmentFromUrl(string url);
        IEnumerable<string> GetAttachmentsFromUrls(params string[] urls);
    }
}