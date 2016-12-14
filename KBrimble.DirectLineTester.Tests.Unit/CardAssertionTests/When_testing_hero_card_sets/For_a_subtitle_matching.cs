using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using KBrimble.DirectLineTester.Assertions.Cards;
using KBrimble.DirectLineTester.Exceptions;
using KBrimble.DirectLineTester.Models.Cards;
using KBrimble.DirectLineTester.Tests.Unit.TestData;
using NUnit.Framework;

namespace KBrimble.DirectLineTester.Tests.Unit.CardAssertionTests.When_testing_hero_card_sets
{
    [TestFixture]
    public class For_a_subtitle_matching
    {
        [TestCase("some text")]
        [TestCase("")]
        [TestCase("symbols ([*])?")]
        public void HasSubtitleMatching_should_pass_if_regex_exactly_matches_message_Subtitle_of_one_card(string cardSubtitleAndRegex)
        {
            var cards = HeroCardTestData.CreateHeroCardSetWithOneCardThatHasSetProperties(subtitle: cardSubtitleAndRegex);

            var sut = new HeroCardSetAssertions(cards);

            Action act = () => sut.HasSubtitleMatching(cardSubtitleAndRegex);

            act.ShouldNotThrow<Exception>();
        }

        [TestCase("some text", "SOME TEXT")]
        [TestCase(@"SYMBOLS ([*])?", @"symbols ([*])?")]
        public void HasSubtitleMatching_should_pass_if_regex_exactly_matches_Subtitle_of_at_least_1_card_regardless_of_case(string cardSubtitle, string regex)
        {
            var cards = HeroCardTestData.CreateHeroCardSetWithOneCardThatHasSetProperties(subtitle: cardSubtitle);

            var sut = new HeroCardSetAssertions(cards);

            Action act = () => sut.HasSubtitleMatching(regex);

            act.ShouldNotThrow<Exception>();
        }

        [TestCase("some text", "so.*xt")]
        [TestCase("some text", "[a-z ]*")]
        [TestCase("some text", "s(ome tex)t")]
        public void HasSubtitleMatching_should_pass_when_using_standard_regex_features(string cardSubtitle, string regex)
        {
            var cards = HeroCardTestData.CreateHeroCardSetWithOneCardThatHasSetProperties(subtitle: cardSubtitle);

            var sut = new HeroCardSetAssertions(cards);

            Action act = () => sut.HasSubtitleMatching(regex);

            act.ShouldNotThrow<Exception>();
        }

        [TestCase("some text!")]
        [TestCase("^[j-z ]*$")]
        [TestCase("s{12}")]
        public void HasSubtitleMatching_should_throw_HeroCardAssertionFailedException_when_regex_matches_no_cards(string regex)
        {
            var cards = HeroCardTestData.CreateRandomHeroCards();

            var sut = new HeroCardSetAssertions(cards);

            Action act = () => sut.HasSubtitleMatching(regex);

            act.ShouldThrow<HeroCardAssertionFailedException>();
        }

        [Test]
        public void HasSubtitleMatching_should_throw_HeroCardAssertionFailedException_when_Subtitle_of_all_cards_is_null()
        {
            var cards = Enumerable.Range(1, 5).Select(_ => new HeroCard()).ToList();

            var sut = new HeroCardSetAssertions(cards);

            Action act = () => sut.HasSubtitleMatching(".*");

            act.ShouldThrow<HeroCardAssertionFailedException>();
        }

        [Test]
        public void HasSubtitleMatching_should_throw_HeroCardAssertionFailedException_when_trying_to_capture_groups_but_Subtitle_of_all_cards_is_null()
        {
            IList<string> matches;

            var cards = Enumerable.Range(1, 5).Select(_ => new HeroCard()).ToList();

            var sut = new HeroCardSetAssertions(cards);

            Action act = () => sut.HasSubtitleMatching(".*", "(.*)", out matches);

            act.ShouldThrow<HeroCardAssertionFailedException>();
        }

        [Test]
        public void HasSubtitleMatching_should_not_output_matches_when_regex_does_not_match_Subtitle_of_any_cards()
        {
            IList<string> matches = null;

            var cards = HeroCardTestData.CreateRandomHeroCards();

            var sut = new HeroCardSetAssertions(cards);

            Action act = () => sut.HasSubtitleMatching("non matching regex", "(some text)", out matches);

            act.ShouldThrow<HeroCardAssertionFailedException>();
            matches.Should().BeNull();
        }

        [Test]
        public void HasSubtitleMatching_should_not_output_matches_when_groupMatchingRegex_does_not_match_Subtitle_of_any_card()
        {
            IList<string> matches;

            var cards = HeroCardTestData.CreateRandomHeroCards();

            var sut = new HeroCardSetAssertions(cards);

            sut.HasSubtitleMatching(".*", "(non matching)", out matches);

            matches.Should().BeNull();
        }

        [Test]
        public void HasSubtitleMatching_should_output_matches_when_groupMatchingRegex_matches_Subtitle_of_any_card()
        {
            IList<string> matches;

            const string someSubtitle = "some text";
            var cards = HeroCardTestData.CreateHeroCardSetWithOneCardThatHasSetProperties(subtitle: someSubtitle);

            var sut = new HeroCardSetAssertions(cards);

            sut.HasSubtitleMatching(someSubtitle, $"({someSubtitle})", out matches);

            matches.First().Should().Be(someSubtitle);
        }

        [Test]
        public void HasSubtitleMatching_should_output_multiple_matches_when_groupMatchingRegex_matches_Subtitle_several_times_for_a_single_card()
        {
            IList<string> matches;

            const string someSubtitle = "some text";
            var cards = HeroCardTestData.CreateHeroCardSetWithOneCardThatHasSetProperties(subtitle: someSubtitle);

            var sut = new HeroCardSetAssertions(cards);

            const string match1 = "some";
            const string match2 = "text";
            sut.HasSubtitleMatching(someSubtitle, $"({match1}) ({match2})", out matches);

            matches.Should().Contain(match1, match2);
        }

        [Test]
        public void HasSubtitleMatching_should_output_multiple_matches_when_groupMatchingRegex_matches_Subtitle_on_multiple_cards()
        {
            IList<string> matches;

            var cards = HeroCardTestData.CreateRandomHeroCards();
            cards.Add(new HeroCard(subtitle: "some text"));
            cards.Add(new HeroCard(subtitle: "same text"));

            var sut = new HeroCardSetAssertions(cards);

            sut.HasSubtitleMatching(".*", @"(s[oa]me) (text)", out matches);

            matches.Should().Contain("some", "same", "text");
        }

        [Test]
        public void HasSubtitleMatching_should_throw_ArgumentNullException_if_regex_is_null()
        {
            var cards = HeroCardTestData.CreateRandomHeroCards();

            var sut = new HeroCardSetAssertions(cards);

            Action act = () => sut.HasSubtitleMatching(null);

            act.ShouldThrow<ArgumentNullException>();
        }

        [Test]
        public void HasSubtitleMatching_should_throw_ArgumentNullException_if_when_capturing_groups_regex_is_null()
        {
            IList<string> matches;

            var cards = HeroCardTestData.CreateRandomHeroCards();

            var sut = new HeroCardSetAssertions(cards);

            Action act = () => sut.HasSubtitleMatching(null, "(.*)", out matches);

            act.ShouldThrow<ArgumentNullException>();
        }

        [Test]
        public void HasSubtitleMatching_should_throw_ArgumentNullException_if_groupMatchRegex_is_null()
        {
            IList<string> matches;

            var cards = HeroCardTestData.CreateRandomHeroCards();

            var sut = new HeroCardSetAssertions(cards);

            Action act = () => sut.HasSubtitleMatching("(.*)", null, out matches);

            act.ShouldThrow<ArgumentNullException>();
        }
    }
}
