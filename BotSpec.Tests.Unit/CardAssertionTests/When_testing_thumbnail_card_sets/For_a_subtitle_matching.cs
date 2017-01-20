using System;
using System.Collections.Generic;
using System.Linq;
using BotSpec.Assertions.Cards;
using BotSpec.Exceptions;
using BotSpec.Tests.Unit.TestData;
using FluentAssertions;
using Microsoft.Bot.Connector.DirectLine;
using NUnit.Framework;

namespace BotSpec.Tests.Unit.CardAssertionTests.When_testing_thumbnail_card_sets
{
    [TestFixture]
    public class For_a_subtitle_matching
    {
        [TestCase("some text")]
        [TestCase("")]
        [TestCase("symbols ([*])?")]
        public void SubtitleMatching_should_pass_if_regex_exactly_matches_message_Subtitle_of_one_card(string cardSubtitleAndRegex)
        {
            var cards = ThumbnailCardTestData.CreateThumbnailCardSetWithOneCardThatHasSetProperties(subtitle: cardSubtitleAndRegex);

            var sut = new ThumbnailCardSetAssertions(cards);

            Action act = () => sut.SubtitleMatching(cardSubtitleAndRegex);

            act.ShouldNotThrow<Exception>();
        }

        [TestCase("some text", "SOME TEXT")]
        [TestCase(@"SYMBOLS ([*])?", @"symbols ([*])?")]
        public void SubtitleMatching_should_pass_if_regex_exactly_matches_Subtitle_of_at_least_1_card_regardless_of_case(string cardSubtitle, string regex)
        {
            var cards = ThumbnailCardTestData.CreateThumbnailCardSetWithOneCardThatHasSetProperties(subtitle: cardSubtitle);

            var sut = new ThumbnailCardSetAssertions(cards);

            Action act = () => sut.SubtitleMatching(regex);

            act.ShouldNotThrow<Exception>();
        }

        [TestCase("some text", "so.*xt")]
        [TestCase("some text", "[a-z ]*")]
        [TestCase("some text", "s(ome tex)t")]
        public void SubtitleMatching_should_pass_when_using_standard_regex_features(string cardSubtitle, string regex)
        {
            var cards = ThumbnailCardTestData.CreateThumbnailCardSetWithOneCardThatHasSetProperties(subtitle: cardSubtitle);

            var sut = new ThumbnailCardSetAssertions(cards);

            Action act = () => sut.SubtitleMatching(regex);

            act.ShouldNotThrow<Exception>();
        }

        [TestCase("some text!")]
        [TestCase("^[j-z ]*$")]
        [TestCase("s{12}")]
        public void SubtitleMatching_should_throw_ThumbnailCardAssertionFailedException_when_regex_matches_no_cards(string regex)
        {
            var cards = ThumbnailCardTestData.CreateRandomThumbnailCards();

            var sut = new ThumbnailCardSetAssertions(cards);

            Action act = () => sut.SubtitleMatching(regex);

            act.ShouldThrow<ThumbnailCardAssertionFailedException>();
        }

        [Test]
        public void SubtitleMatching_should_throw_ThumbnailCardAssertionFailedException_when_Subtitle_of_all_cards_is_null()
        {
            var cards = Enumerable.Range(1, 5).Select(_ => new ThumbnailCard()).ToList();

            var sut = new ThumbnailCardSetAssertions(cards);

            Action act = () => sut.SubtitleMatching(".*");

            act.ShouldThrow<ThumbnailCardAssertionFailedException>();
        }

        [Test]
        public void SubtitleMatching_should_throw_ThumbnailCardAssertionFailedException_when_trying_to_capture_groups_but_Subtitle_of_all_cards_is_null()
        {
            IList<string> matches;

            var cards = Enumerable.Range(1, 5).Select(_ => new ThumbnailCard()).ToList();

            var sut = new ThumbnailCardSetAssertions(cards);

            Action act = () => sut.SubtitleMatching(".*", "(.*)", out matches);

            act.ShouldThrow<ThumbnailCardAssertionFailedException>();
        }

        [Test]
        public void SubtitleMatching_should_not_output_matches_when_regex_does_not_match_Subtitle_of_any_cards()
        {
            IList<string> matches = null;

            var cards = ThumbnailCardTestData.CreateRandomThumbnailCards();

            var sut = new ThumbnailCardSetAssertions(cards);

            Action act = () => sut.SubtitleMatching("non matching regex", "(some text)", out matches);

            act.ShouldThrow<ThumbnailCardAssertionFailedException>();
            matches.Should().BeNull();
        }

        [Test]
        public void SubtitleMatching_should_not_output_matches_when_groupMatchingRegex_does_not_match_Subtitle_of_any_card()
        {
            IList<string> matches;

            var cards = ThumbnailCardTestData.CreateRandomThumbnailCards();

            var sut = new ThumbnailCardSetAssertions(cards);

            sut.SubtitleMatching(".*", "(non matching)", out matches);

            matches.Should().BeNull();
        }

        [Test]
        public void SubtitleMatching_should_output_matches_when_groupMatchingRegex_matches_Subtitle_of_any_card()
        {
            IList<string> matches;

            const string someSubtitle = "some text";
            var cards = ThumbnailCardTestData.CreateThumbnailCardSetWithOneCardThatHasSetProperties(subtitle: someSubtitle);

            var sut = new ThumbnailCardSetAssertions(cards);

            sut.SubtitleMatching(someSubtitle, $"({someSubtitle})", out matches);

            matches.First().Should().Be(someSubtitle);
        }

        [Test]
        public void SubtitleMatching_should_output_multiple_matches_when_groupMatchingRegex_matches_Subtitle_several_times_for_a_single_card()
        {
            IList<string> matches;

            const string someSubtitle = "some text";
            var cards = ThumbnailCardTestData.CreateThumbnailCardSetWithOneCardThatHasSetProperties(subtitle: someSubtitle);

            var sut = new ThumbnailCardSetAssertions(cards);

            const string match1 = "some";
            const string match2 = "text";
            sut.SubtitleMatching(someSubtitle, $"({match1}) ({match2})", out matches);

            matches.Should().Contain(match1, match2);
        }

        [Test]
        public void SubtitleMatching_should_output_multiple_matches_when_groupMatchingRegex_matches_Subtitle_on_multiple_cards()
        {
            IList<string> matches;

            var cards = ThumbnailCardTestData.CreateRandomThumbnailCards();
            cards.Add(new ThumbnailCard(subtitle: "some text"));
            cards.Add(new ThumbnailCard(subtitle: "same text"));

            var sut = new ThumbnailCardSetAssertions(cards);

            sut.SubtitleMatching(".*", @"(s[oa]me) (text)", out matches);

            matches.Should().Contain("some", "same", "text");
        }

        [Test]
        public void SubtitleMatching_should_throw_ArgumentNullException_if_regex_is_null()
        {
            var cards = ThumbnailCardTestData.CreateRandomThumbnailCards();

            var sut = new ThumbnailCardSetAssertions(cards);

            Action act = () => sut.SubtitleMatching(null);

            act.ShouldThrow<ArgumentNullException>();
        }

        [Test]
        public void SubtitleMatching_should_throw_ArgumentNullException_if_when_capturing_groups_regex_is_null()
        {
            IList<string> matches;

            var cards = ThumbnailCardTestData.CreateRandomThumbnailCards();

            var sut = new ThumbnailCardSetAssertions(cards);

            Action act = () => sut.SubtitleMatching(null, "(.*)", out matches);

            act.ShouldThrow<ArgumentNullException>();
        }

        [Test]
        public void SubtitleMatching_should_throw_ArgumentNullException_if_groupMatchRegex_is_null()
        {
            IList<string> matches;

            var cards = ThumbnailCardTestData.CreateRandomThumbnailCards();

            var sut = new ThumbnailCardSetAssertions(cards);

            Action act = () => sut.SubtitleMatching("(.*)", null, out matches);

            act.ShouldThrow<ArgumentNullException>();
        }
    }
}
