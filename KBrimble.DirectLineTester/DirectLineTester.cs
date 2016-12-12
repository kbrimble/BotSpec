using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Bot.Connector.DirectLine.Models;

namespace KBrimble.DirectLineTester
{
    public class DirectLineTester
    {
        private readonly IBotClient _botClient;
        private bool _getMessagesFromHighWatermark;

        public DirectLineTester()
        {
            _botClient = new BotClient(DirectLineTesterSettings.TokenOrSecret);
            _getMessagesFromHighWatermark = false;
        }

        public async Task SendMessage(string message)
        {
            await _botClient.SendMessage(message).ConfigureAwait(false);
            _getMessagesFromHighWatermark = true;
        }

        public async Task Expect(Action<IEnumerable<Message>> messageSetAssertion)
        {
            var messages = _getMessagesFromHighWatermark
                ? await _botClient.GetMessagesFromHigherWatermark().ConfigureAwait(false)
                : await _botClient.GetMessagesFromLowerWatermark().ConfigureAwait(false);

            messageSetAssertion(messages);

            _getMessagesFromHighWatermark = false;
        }
    }
}
