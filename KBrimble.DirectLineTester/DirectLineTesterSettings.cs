using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KBrimble.DirectLineTester.Attachments;

namespace KBrimble.DirectLineTester
{
    public static class DirectLineTesterSettings
    {
        private static IAttachmentRetreiver _attachmentRetriever;

        private static IAttachmentRetreiver AttachmentRetriever 
        {
            get { return _attachmentRetriever ?? (_attachmentRetriever = new AttachmentRetriever()); }
            set { _attachmentRetriever = value; }
        }

        public static string TokenOrSecret { get; set; }
    }
}
