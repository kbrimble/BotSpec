using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Bot.Connector.DirectLine;
using Microsoft.Bot.Connector.DirectLine.Models;

namespace KBrimble.DirectLineTester
{
    internal class BotClient : IBotClient
    {
        private readonly IDirectLineClient _directLineClient;
        private Conversation _conversation;
        private string _higherWatermark;
        private string _lowerWatermark;

        public BotClient(string secretOrToken)
        {
            _directLineClient = new DirectLineClient(secretOrToken);
        }

        public async Task StartConversation()
        {
            _conversation = await _directLineClient.Conversations.NewConversationAsync().ConfigureAwait(false);
        }

        public async Task SendMessage(string messageText)
        {
            var message = new Message
            {
                Text = messageText,
                ConversationId = _conversation.ConversationId
            };
            await SendMessage(message).ConfigureAwait(false);
        }

        public async Task SendMessage(Message message)
        {
            await _directLineClient.Conversations.PostMessageAsync(_conversation.ConversationId, message).ConfigureAwait(false);
        }

        public async Task<IEnumerable<Message>> GetMessagesFromHigherWatermark()
        {
            var messageSet = await GetMessageSet(_higherWatermark).ConfigureAwait(false);
            _lowerWatermark = _higherWatermark;
            _higherWatermark = messageSet.Watermark;
            return messageSet.Messages;
        }

        public async Task<IEnumerable<Message>> GetMessagesFromLowerWatermark()
        {
            return (await GetMessageSet(_lowerWatermark).ConfigureAwait(false)).Messages;
        }

        private async Task<MessageSet> GetMessageSet(string watermark)
        {
            return await _directLineClient.Conversations.GetMessagesAsync(_conversation.ConversationId, watermark).ConfigureAwait(false);
        }
    }
}