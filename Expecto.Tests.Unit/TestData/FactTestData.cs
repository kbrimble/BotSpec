using System.Collections.Generic;
using Expecto.Models.Cards;

namespace Expecto.Tests.Unit.TestData
{
    public class FactTestData
    {
        internal static IList<Fact> CreateFactSetWithOneFactThatHasSetProperties(string key = default(string), string value = default(string))
        {
            var matchingFact = new Fact(key, value);
            var cards = CreateRandomFacts();
            cards.Add(matchingFact);
            return cards;
        }

        internal static List<Fact> CreateRandomFacts()
        {
            var cardImages = new List<Fact>();
            for (var i = 0; i < 5; i++)
            {
                cardImages.Add(new Fact($"key{i}", $"value{i}"));
            }
            return cardImages;
        }
    }
}