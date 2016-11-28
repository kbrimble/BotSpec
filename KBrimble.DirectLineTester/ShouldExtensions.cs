using System.Collections.Generic;
using KBrimble.DirectLineTester.Assertions.Attachments;
using KBrimble.DirectLineTester.Assertions.Cards;
using KBrimble.DirectLineTester.Assertions.Messages;
using KBrimble.DirectLineTester.Models.Cards;
using Microsoft.Bot.Connector.DirectLine.Models;

namespace KBrimble.DirectLineTester
{
    public static class ShouldExtensions
    {
        public static IMessageAssertions Should(this IEnumerable<Message> messageSet) => new MessageSetAssertions(messageSet);
        public static IMessageAssertions Should(this MessageSet messageSet) => new MessageSetAssertions(messageSet);
        public static IMessageAssertions Should(this Message message) => new MessageAssertions(message);
        public static IThumbnailCardAssertions Should(this ThumbnailCard thumbnailCard) => new ThumbnailCardAssertions(thumbnailCard);
        public static IMessageAttachmentAssertions ShouldHaveAttachment(this Message message) => new MessageAttachmentAssertions(message);
        public static IMessageAttachmentAssertions ShouldHaveAttachment(this MessageSet messageSet) => new MessageSetAttachmentAssertions(messageSet);
        public static IMessageAttachmentAssertions ShouldHaveAttachment(this IEnumerable<Message> messageSet) => new MessageSetAttachmentAssertions(messageSet);
    }
}
