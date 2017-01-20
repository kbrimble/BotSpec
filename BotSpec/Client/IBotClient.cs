using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Bot.Connector.DirectLine;

namespace BotSpec.Client
{
    public interface IBotClient
    {
        void StartConversation();
        void SendMessage(string messageText, object channelData = null);
        void SendMessage(Activity message);
        IList<Activity> GetActivitiesFromHigherWatermark(int expectedNumberofActivities);
        IList<Activity> GetActivitiesFromLowerWatermark(int expectedNumberofActivities);
    }
}