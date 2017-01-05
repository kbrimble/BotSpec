using System;
using System.Collections.Generic;
using System.Net;

namespace BotSpec.Attachments
{
    internal class DefaultAttachmentRetriever : IAttachmentRetriever
    {
        public string GetAttachmentFromUrl(string url)
        {
            using (var wc = new WebClient())
            {
                try
                {
                    return wc.DownloadString(url);
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }

        public IEnumerable<string> GetAttachmentsFromUrls(params string[] urls)
        {
            using (var wc = new WebClient())
            {
                foreach (var url in urls)
                {
                    string json;
                    try
                    {
                        json = wc.DownloadString(url);
                    }
                    catch (Exception)
                    {
                        continue;
                    }
                    yield return json;
                }
            }
        }
    }
}