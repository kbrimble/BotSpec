using System.Collections.Generic;
using BotSpec.Models.Cards;

namespace BotSpec.Assertions.Cards.CardComponents
{
    public interface ICardActionAssertions
    {
        ICardActionAssertions TitleMatching(string regex);
        ICardActionAssertions TitleMatching(string regex, string groupMatchingRegex, out IList<string> groupMatches);
        ICardActionAssertions ValueMatching(string regex);
        ICardActionAssertions ValueMatching(string regex, string groupMatchingRegex, out IList<string> groupMatches);
        ICardActionAssertions ImageMatching(string regex);
        ICardActionAssertions ImageMatching(string regex, string groupMatchingRegex, out IList<string> groupMatches);
        ICardActionAssertions ActionType(CardActionType type);
    }
}