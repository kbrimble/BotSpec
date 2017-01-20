using System.Collections.Generic;
using BotSpec.Assertions.Cards;
using Microsoft.Bot.Connector.DirectLine;

namespace BotSpec.Assertions.Attachments
{
    internal class ActivitySetAttachmentAssertions : IActivityAttachmentAssertions
    {
        private readonly IEnumerable<Activity> _activitySet;

        public ActivitySetAttachmentAssertions(IEnumerable<Activity> activitySet)
        {
            _activitySet = activitySet;
        }

        public IThumbnailCardAssertions OfTypeThumbnailCard()
        {
            return new ThumbnailCardSetAssertions(_activitySet);
        }

        public IHeroCardAssertions OfTypeHeroCard()
        {
            return new HeroCardSetAssertions(_activitySet);
        }

        public ISigninCardAssertions OfTypeSigninCard()
        {
            return new SigninCardSetAssertions(_activitySet);
        }

        public IReceiptCardAssertions OfTypeReceiptCard()
        {
            return new ReceiptCardSetAssertions(_activitySet);
        }
    }
}