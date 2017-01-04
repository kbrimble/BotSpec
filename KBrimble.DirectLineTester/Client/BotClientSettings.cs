using System;

namespace KBrimble.DirectLineTester.Client
{
    public static class BotClientSettings
    {
        public static BotClientType ClientType { get; set; }
        public static Func<string, IBotClient> CustomBotClientFactoryMethod { get; set; }
    }
}