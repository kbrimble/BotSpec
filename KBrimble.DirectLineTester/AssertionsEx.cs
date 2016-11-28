using System.Collections.Generic;
using KBrimble.DirectLineTester.Assertions;
using Microsoft.Bot.Connector.DirectLine.Models;

namespace KBrimble.DirectLineTester
{
    public static class AssertionsEx
    {
        public static IMessageAssertions Should(this IEnumerable<Message> messageSet) => new MessageSetAssertions(messageSet);
        public static IMessageAssertions Should(this MessageSet messageSet) => new MessageSetAssertions(messageSet);
        public static IMessageAssertions Should(this Message message) => new MessageAssertions(message);
        public static IThumbnailCardAssertions Should(this ThumbnailCard thumbnailCard) => new ThumbnailCardAssertions(thumbnailCard);
    }
}
