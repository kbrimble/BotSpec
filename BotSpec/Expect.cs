using BotSpec.Assertions.Activities;
using BotSpec.Client;

namespace BotSpec
{
    public class Expect
    {
        private readonly IBotClient _botClient;
        private bool _fetchFromHighWatermark = true;

        public Expect(string secretOrToken)
        {
            _botClient = BotClientFactory.GetBotClient(secretOrToken);
            _botClient.StartConversation();
        }

        public void SendActivity(string message, object channelData = null)
        {
            _botClient.SendMessage(message, channelData);
            _fetchFromHighWatermark = true;
        }

        public IActivityAssertions Activity(int expectedNumberofActivities = 0)
        {
            var messages = _fetchFromHighWatermark ?
                _botClient.GetActivitiesFromHigherWatermark(expectedNumberofActivities) : 
                _botClient.GetActivitiesFromLowerWatermark(expectedNumberofActivities);

            _fetchFromHighWatermark = false;

            return new ActivitySetAssertions(messages);
        }
    }
}