using System;

namespace BotSpec.Client
{
    internal static class BotClientFactory
    {
        public static IBotClient GetBotClient(string secretOrToken)
        {
            switch (BotClientSettings.ClientType)
            {
                case BotClientType.Default:
                    return new DefaultBotClient(secretOrToken);
                case BotClientType.Custom:
                    if (BotClientSettings.CustomBotClientFactoryMethod == null)
                        throw new InvalidOperationException("Custom bot client selected but one hasn't been set");
                    return BotClientSettings.CustomBotClientFactoryMethod(secretOrToken);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}