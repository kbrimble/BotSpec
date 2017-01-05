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
            _botClient.StartConversation().RunSynchronously();
        }

        public void SendMessage(string message)
        {
            _fetchFromHighWatermark = true;
            _botClient.SendMessage(message).RunSynchronously();
        }

        public IMessageAssertions Message()
        {
            var messages = _fetchFromHighWatermark ?
                _botClient.GetMessagesFromHigherWatermark().Result : 
                _botClient.GetMessagesFromLowerWatermark().Result;

            _fetchFromHighWatermark = false;

            return new MessageSetAssertions(messages);
        }
    }
}