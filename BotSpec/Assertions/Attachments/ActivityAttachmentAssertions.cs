using BotSpec.Assertions.Cards;
using Microsoft.Bot.Connector.DirectLine;

namespace BotSpec.Assertions.Attachments
{
    internal class ActivityAttachmentAssertions : IActivityAttachmentAssertions
    {
        private readonly Activity _activity;

        public ActivityAttachmentAssertions(Activity activity)
        {
            _activity = activity;
        }

        public IThumbnailCardAssertions OfTypeThumbnailCard()
        {
            return new ThumbnailCardSetAssertions(_activity);
        }

        public IHeroCardAssertions OfTypeHeroCard()
        {
            return new HeroCardSetAssertions(_activity);
        }

        public ISigninCardAssertions OfTypeSigninCard()
        {
            return new SigninCardSetAssertions(_activity);
        }

        public IReceiptCardAssertions OfTypeReceiptCard()
        {
            return new ReceiptCardSetAssertions(_activity);
        }
    }
}