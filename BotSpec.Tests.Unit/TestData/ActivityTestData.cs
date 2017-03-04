using System;
using System.Collections.Generic;
using Microsoft.Bot.Connector.DirectLine;

namespace BotSpec.Tests.Unit.TestData
{
    public class ActivityTestData
    {
        internal static IList<Activity> CreateActivitySetWithOneActivityThatHasSetProperties(string id = null, string conversationId = null, DateTime? created = null, string fromProperty = null, string text = null, object channelData = null, IList<Attachment> attachments = null, string eTag = null)
        {
            var conversation = new ConversationAccount(id: conversationId);
            var from = new ChannelAccount(name: fromProperty);

            var matchingActivity = new Activity(id: id, conversation: conversation, timestamp: created ?? DateTime.UtcNow, fromProperty: from, text: text,
                channelData: channelData, attachments: attachments);
            var activities = CreateRandomActivities();
            activities.Add(matchingActivity);
            return activities;
        }

        internal static List<Activity> CreateRandomActivities()
        {
            var activities = new List<Activity>();
            var now = DateTime.UtcNow;
            for (var i = 0; i < 5; i++)
            {
                var dt = now.AddSeconds(-1);
                var conversation = new ConversationAccount(id: $"conversationId{i}");
                var from = new ChannelAccount(name: $"fromProperty{i}");
                activities.Add(new Activity(id: $"id{i}", conversation: conversation, timestamp: dt, fromProperty: from, text: $"text{i}"));
            }
            return activities;
        }
    }
}