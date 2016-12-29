using System.Collections.Generic;

namespace KBrimble.DirectLineTester.Attachments
{
    public interface IAttachmentRetriever
    {
        string GetAttachmentFromUrl(string url);
        IEnumerable<string> GetAttachmentsFromUrls(params string[] urls);
    }
}