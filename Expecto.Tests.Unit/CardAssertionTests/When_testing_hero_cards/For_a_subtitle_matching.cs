using System;
using System.Collections.Generic;
using System.Linq;
using Expecto.Assertions.Cards;
using Expecto.Exceptions;
using Expecto.Models.Cards;
using FluentAssertions;
using NUnit.Framework;

namespace Expecto.Tests.Unit.CardAssertionTests.When_testing_hero_cards
{
    [TestFixture]
    public class For_a_subtitle_matching
    {
        [TestCase("some text")]
        [TestCase("")]
        [TestCase("symbols ([*])?")]
        public void SubtitleMatching_should_pass_if_regex_exactly_matches_message_Subtitle(string cardSubtitleAndRegex)
        {
            var heroCard = new HeroCard(subtitle: cardSubtitleAndRegex);

            var sut = new HeroCardAssertions(heroCard);

            Action act = () => sut.SubtitleMatching(cardSubtitleAndRegex);

            act.ShouldNotThrow<Exception>();
        }

        [TestCase("some text", "SOME TEXT")]
        [TestCase(@"SYMBOLS ([*])?", @"symbols ([*])?")]
        public void SubtitleMatching_should_pass_regardless_of_case(string cardSubtitle, string regex)
        {
            var heroCard = new HeroCard(subtitle: cardSubtitle);

            var sut = new HeroCardAssertions(heroCard);

            Action act = () => sut.SubtitleMatching(regex);

            act.ShouldNotThrow<Exception>();
        }

        [TestCase("some text", "so.*xt")]
        [TestCase("some text", "[a-z ]*")]
        [TestCase("some text", "s(ome tex)t")]
        public void SubtitleMatching_should_pass_when_using_standard_regex_features(string cardSubtitle, string regex)
        {
            var heroCard = new HeroCard(subtitle: cardSubtitle);

            var sut = new HeroCardAssertions(heroCard);

            Action act = () => sut.SubtitleMatching(regex);

            act.ShouldNotThrow<Exception>();
        }

        [TestCase("some text", "some text!")]
        [TestCase("some text", "^[j-z ]*$")]
        [TestCase("some text", "s{12}")]
        public void SubtitleMatching_should_throw_HeroCardAssertionFailedException_for_non_matching_regexes(string cardSubtitle, string regex)
        {
            var heroCard = new HeroCard(subtitle: cardSubtitle);

            var sut = new HeroCardAssertions(heroCard);

            Action act = () => sut.SubtitleMatching(regex);

            act.ShouldThrow<HeroCardAssertionFailedException>();
        }

        [Test]
        public void SubtitleMatching_should_not_output_matches_when_regex_does_not_match_text()
        {
            IList<string> matches = null;

            var heroCard = new HeroCard(subtitle: "some text");

            var sut = new HeroCardAssertions(heroCard);

            Action act = () => sut.SubtitleMatching("non matching regex", "(some text)", out matches);

            act.ShouldThrow<HeroCardAssertionFailedException>();
            matches.Should().BeNull();
        }

        [Test]
        public void SubtitleMatching_should_not_output_matches_when_groupMatchingRegex_does_not_match_text()
        {
            IList<string> matches;

            var heroCard = new HeroCard(subtitle: "some text");

            var sut = new HeroCardAssertions(heroCard);

            sut.SubtitleMatching("some text", "(non matching)", out matches);

            matches.Should().BeNull();
        }

        [Test]
        public void SubtitleMatching_should_output_matches_when_groupMatchingRegex_matches_text()
        {
            IList<string> matches;

            const string someText = "some text";
            var heroCard = new HeroCard(subtitle: someText);

            var sut = new HeroCardAssertions(heroCard);

            sut.SubtitleMatching(someText, $"({someText})", out matches);

            matches.First().Should().Be(someText);
        }

        [Test]
        public void SubtitleMatching_should_output_multiple_matches_when_groupMatchingRegex_matches_text_several_times()
        {
            IList<string> matches;

            const string someText = "some text";
            var heroCard = new HeroCard(subtitle: someText);

            var sut = new HeroCardAssertions(heroCard);

            const string match1 = "some";
            const string match2 = "text";
            sut.SubtitleMatching(someText, $"({match1}) ({match2})", out matches);

            matches.Should().Contain(match1, match2);
        }

        [Test]
        public void SubtitleMatching_should_throw_HeroCardAssertionFailedException_when_text_is_null()
        {
            var card = new HeroCard();

            var sut = new HeroCardAssertions(card);

            Action act = () => sut.SubtitleMatching("anything");

            act.ShouldThrow<HeroCardAssertionFailedException>();
        }

        [Test]
        public void SubtitleMatching_should_throw_HeroCardAssertionFailedException_when_trying_to_capture_groups_but_text_is_null()
        {
            IList<string> matches;
            var card = new HeroCard();

            var sut = new HeroCardAssertions(card);

            Action act = () => sut.SubtitleMatching("anything", "(.*)", out matches);

            act.ShouldThrow<HeroCardAssertionFailedException>();
        }

        [Test]
        public void SubtitleMatching_should_throw_ArgumentNullException_if_regex_is_null()
        {
            var card = new HeroCard();

            var sut = new HeroCardAssertions(card);

            Action act = () => sut.SubtitleMatching(null);

            act.ShouldThrow<ArgumentNullException>();
        }

        [Test]
        public void SubtitleMatching_should_throw_ArgumentNullException_if_when_capturing_groups_regex_is_null()
        {
            IList<string> matches;

            var card = new HeroCard();

            var sut = new HeroCardAssertions(card);

            Action act = () => sut.SubtitleMatching(null, "(.*)", out matches);

            act.ShouldThrow<ArgumentNullException>();
        }

        [Test]
        public void SubtitleMatching_should_throw_ArgumentNullException_if_groupMatchRegex_is_null()
        {
            IList<string> matches;

            var card = new HeroCard();

            var sut = new HeroCardAssertions(card);

            Action act = () => sut.SubtitleMatching("(.*)", null, out matches);

            act.ShouldThrow<ArgumentNullException>();
        }
    }
}
