using System;

namespace BotSpec.Client
{
    public static class BotClientSettings
    {
        public static BotClientType ClientType { get; set; }
        public static Func<string, IBotClient> CustomBotClientFactoryMethod { get; set; }
    }
}