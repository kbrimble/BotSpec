using BotSpec.Assertions.Messages;
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
            _botClient.StartConversation().Wait();
        }

        public void SendMessage(string message, object channelData = null)
        {
            _fetchFromHighWatermark = true;
            _botClient.SendMessage(message, channelData).Wait();
        }

        public IMessageAssertions Message()
        {
            var messages = _fetchFromHighWatermark ?
                _botClient.GetMessagesFromHigherWatermark().ConfigureAwait(false).GetAwaiter().GetResult() : 
                _botClient.GetMessagesFromLowerWatermark().ConfigureAwait(false).GetAwaiter().GetResult();

            _fetchFromHighWatermark = false;

            return new MessageSetAssertions(messages);
        }
    }
}