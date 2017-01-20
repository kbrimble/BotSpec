using System.Collections.Generic;
using System.Threading;
using Microsoft.Bot.Connector.DirectLine;

namespace BotSpec.Client
{
    internal class DefaultBotClient : IBotClient
    {
        private const int RetryTimes = 10;
        private const int WaitTimeMs = 1000;

        private readonly IDirectLineClient _directLineClient;
        private Conversation _conversation;
        private ChannelAccount _channelAccount;
        private string _higherWatermark;
        private string _lowerWatermark;

        public DefaultBotClient(string secretOrToken)
        {
            _directLineClient = new DirectLineClient(secretOrToken);
        }

        public void StartConversation()
        {
            _conversation = _directLineClient.Conversations.StartConversation();
            _channelAccount = new ChannelAccount($"BotSpec{_conversation.ConversationId.Substring(0, 8)}");
        }

        public void SendMessage(string messageText, object channelData = null)
        {
            var message = new Activity
            {
                Text = messageText,
                From = _channelAccount,
                Type = "message"
            };
            if (channelData != null)
                message.ChannelData = channelData;
            SendMessage(message);
        }

        public void SendMessage(Activity activity)
        {
            _directLineClient.Conversations.PostActivity(_conversation.ConversationId, activity);
        }

        public IList<Activity> GetActivitiesFromHigherWatermark(int expectedNumberofActivities)
        {
            var activities = GetActivitiesWithRetry(expectedNumberofActivities, _higherWatermark);

            if (activities?.Watermark == null)
                return activities?.Activities;

            _lowerWatermark = _higherWatermark;
            _higherWatermark = activities.Watermark;

            return activities.Activities;
        }

        public IList<Activity> GetActivitiesFromLowerWatermark(int expectedNumberofActivities)
        {
            var activitySet = GetActivitiesWithRetry(expectedNumberofActivities, _lowerWatermark);
            return activitySet.Activities;
        }

        private ActivitySet GetActivitiesWithRetry(int expectedNumberofActivities, string watermark)
        {
            var noOfRetries = RetryTimes;
            ActivitySet activitySet = null;
            var retry = true;
            while (noOfRetries > 0 && retry)
            {
                var latestSet = GetActivitySet(watermark);
                var newActivities = NumberOfNewActivities(latestSet, watermark);

                if (expectedNumberofActivities > 0 && newActivities < expectedNumberofActivities)
                {
                    noOfRetries -= 1;
                    Thread.Sleep(WaitTimeMs);
                    continue;
                }

                activitySet = latestSet;

                retry = false;
            }
            return activitySet;
        }

        private ActivitySet GetActivitySet(string watermark)
        {
            return watermark == null
                ? _directLineClient.Conversations.GetActivities(_conversation.ConversationId)
                : _directLineClient.Conversations.GetActivities(_conversation.ConversationId, watermark);
        }

        private static int NumberOfNewActivities(ActivitySet activitySet, string watermark)
        {
            if (activitySet?.Watermark == null)
                return 0;
            return int.Parse(activitySet.Watermark ?? "0") - int.Parse(watermark ?? "0");
        }
    }
}