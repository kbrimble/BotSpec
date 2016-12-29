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
        private static IAttachmentRetriever _attachmentRetriever;

        private static IAttachmentRetriever AttachmentRetriever 
        {
            get { return _attachmentRetriever ?? (_attachmentRetriever = new DefaultAttachmentRetriever()); }
            set { _attachmentRetriever = value; }
        }

        public static string TokenOrSecret { get; set; }
    }
}
