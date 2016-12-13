using System;
using System.Collections.Generic;
using Microsoft.Bot.Connector.DirectLine.Models;

namespace KBrimble.DirectLineTester.Tests.Unit.TestData
{
    public class MessageTestData
    {
        internal static IEnumerable<Message> CreateMessageSetWithOneMessageThatHasSetProperties(string id = null, string conversationId = null, DateTime? created = null, string fromProperty = null, string text = null, object channelData = null, IList<string> images = null, IList<Attachment> attachments = null, string eTag = null)
        {
            var matchingMessage = new Message(id, conversationId, created ?? DateTime.UtcNow, fromProperty, text, channelData, images, attachments, eTag);
            var messages = CreateRandomMessages();
            messages.Add(matchingMessage);
            return messages;
        }

        internal static List<Message> CreateRandomMessages()
        {
            var messages = new List<Message>();
            var now = DateTime.UtcNow;
            for (var i = 0; i < 5; i++)
            {
                var dt = now.AddSeconds(-1);
                messages.Add(new Message($"id{i}", $"conversationId{i}", dt, $"fromProperty{i}", $"text{i}", null, null, null, $"eTag"));
            }
            return messages;
        }
    }
}