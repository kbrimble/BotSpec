using KBrimble.DirectLineTester.Assertions.Messages;
using KBrimble.DirectLineTester.Client;

namespace KBrimble.DirectLineTester
{
    public class Expecto
    {
        private readonly IBotClient _botClient;
        private bool _fetchFromHighWatermark = true;

        public Expecto(string secretOrToken)
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