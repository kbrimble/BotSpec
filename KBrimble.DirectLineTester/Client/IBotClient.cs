using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Bot.Connector.DirectLine.Models;

namespace KBrimble.DirectLineTester.Client
{
    public interface IBotClient
    {
        Task StartConversation();
        Task SendMessage(string messageText);
        Task SendMessage(Message message);
        Task<IEnumerable<Message>> GetMessagesFromHigherWatermark();
        Task<IEnumerable<Message>> GetMessagesFromLowerWatermark();
    }
}