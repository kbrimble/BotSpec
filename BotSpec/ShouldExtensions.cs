using BotSpec.Assertions.Activities;
using BotSpec.Assertions.Cards;
using BotSpec.Assertions.Cards.CardComponents;
using Microsoft.Bot.Connector.DirectLine;

namespace BotSpec
{
    internal static class ShouldExtensions
    {
        public static IActivityAssertions Should(this Activity message) => new ActivityAssertions(message);
        public static IThumbnailCardAssertions That(this ThumbnailCard thumbnailCard) => new ThumbnailCardAssertions(thumbnailCard);
        public static ISigninCardAssertions That(this SigninCard signinCard) => new SigninCardAssertions(signinCard);
        public static IReceiptCardAssertions That(this ReceiptCard receiptCard) => new ReceiptCardAssertions(receiptCard);
        public static IHeroCardAssertions That(this HeroCard heroCard) => new HeroCardAssertions(heroCard);
        public static ICardImageAssertions That(this CardImage cardImage) => new CardImageAssertions(cardImage);
        public static ICardActionAssertions That(this CardAction cardAction) => new CardActionAssertions(cardAction);
        public static IFactAssertions That(this Fact fact) => new FactAssertions(fact);
        public static IReceiptItemAssertions That(this ReceiptItem receiptItem) => new ReceiptItemAssertions(receiptItem);
    }
}
