using System;
using System.Collections.Generic;
using System.Linq;
using BotSpec.Assertions.Cards;
using BotSpec.Exceptions;
using BotSpec.Models.Cards;
using FluentAssertions;
using NUnit.Framework;

namespace BotSpec.Tests.Unit.CardAssertionTests.When_testing_thumbnail_cards
{
    [TestFixture]
    public class For_text_matching
    {
        [TestCase("some text")]
        [TestCase("")]
        [TestCase("symbols ([*])?")]
        public void TextMatching_should_pass_if_regex_exactly_matches_message_Text(string cardTextAndRegex)
        {
            var thumbnailCard = new ThumbnailCard(text: cardTextAndRegex);

            var sut = new ThumbnailCardAssertions(thumbnailCard);

            Action act = () => sut.TextMatching(cardTextAndRegex);

            act.ShouldNotThrow<Exception>();
        }

        [TestCase("some text", "SOME TEXT")]
        [TestCase(@"SYMBOLS ([*])?", @"symbols ([*])?")]
        public void TextMatching_should_pass_regardless_of_case(string cardText, string regex)
        {
            var thumbnailCard = new ThumbnailCard(text: cardText);

            var sut = new ThumbnailCardAssertions(thumbnailCard);

            Action act = () => sut.TextMatching(regex);

            act.ShouldNotThrow<Exception>();
        }

        [TestCase("some text", "so.*xt")]
        [TestCase("some text", "[a-z ]*")]
        [TestCase("some text", "s(ome tex)t")]
        public void TextMatching_should_pass_when_using_standard_regex_features(string cardText, string regex)
        {
            var thumbnailCard = new ThumbnailCard(text: cardText);

            var sut = new ThumbnailCardAssertions(thumbnailCard);

            Action act = () => sut.TextMatching(regex);

            act.ShouldNotThrow<Exception>();
        }

        [TestCase("some text", "some text!")]
        [TestCase("some text", "^[j-z ]*$")]
        [TestCase("some text", "s{12}")]
        public void TextMatching_should_throw_ThumbnailCardAssertionFailedException_for_non_matching_regexes(string cardText, string regex)
        {
            var thumbnailCard = new ThumbnailCard(text: cardText);

            var sut = new ThumbnailCardAssertions(thumbnailCard);

            Action act = () => sut.TextMatching(regex);

            act.ShouldThrow<ThumbnailCardAssertionFailedException>();
        }

        [Test]
        public void TextMatching_should_not_output_matches_when_regex_does_not_match_text()
        {
            IList<string> matches = null;

            var thumbnailCard = new ThumbnailCard(text: "some text");

            var sut = new ThumbnailCardAssertions(thumbnailCard);

            Action act = () => sut.TextMatching("non matching regex", "(some text)", out matches);

            act.ShouldThrow<ThumbnailCardAssertionFailedException>();
            matches.Should().BeNull();
        }

        [Test]
        public void TextMatching_should_not_output_matches_when_groupMatchingRegex_does_not_match_text()
        {
            IList<string> matches;

            var thumbnailCard = new ThumbnailCard(text: "some text");

            var sut = new ThumbnailCardAssertions(thumbnailCard);

            sut.TextMatching("some text", "(non matching)", out matches);

            matches.Should().BeNull();
        }

        [Test]
        public void TextMatching_should_output_matches_when_groupMatchingRegex_matches_text()
        {
            IList<string> matches;

            const string someText = "some text";
            var thumbnailCard = new ThumbnailCard(text: someText);

            var sut = new ThumbnailCardAssertions(thumbnailCard);

            sut.TextMatching(someText, $"({someText})", out matches);

            matches.First().Should().Be(someText);
        }

        [Test]
        public void TextMatching_should_output_multiple_matches_when_groupMatchingRegex_matches_text_several_times()
        {
            IList<string> matches;

            const string someText = "some text";
            var thumbnailCard = new ThumbnailCard(text: someText);

            var sut = new ThumbnailCardAssertions(thumbnailCard);

            const string match1 = "some";
            const string match2 = "text";
            sut.TextMatching(someText, $"({match1}) ({match2})", out matches);

            matches.Should().Contain(match1, match2);
        }

        [Test]
        public void TextMatching_should_throw_ThumbnailCardAssertionFailedException_when_text_is_null()
        {
            var card = new ThumbnailCard();

            var sut = new ThumbnailCardAssertions(card);

            Action act = () => sut.TextMatching("anything");

            act.ShouldThrow<ThumbnailCardAssertionFailedException>();
        }

        [Test]
        public void TextMatching_should_throw_ThumbnailCardAssertionFailedException_when_trying_to_capture_groups_but_text_is_null()
        {
            IList<string> matches;
            var card = new ThumbnailCard();

            var sut = new ThumbnailCardAssertions(card);

            Action act = () => sut.TextMatching("anything", "(.*)", out matches);

            act.ShouldThrow<ThumbnailCardAssertionFailedException>();
        }

        [Test]
        public void TextMatching_should_throw_ArgumentNullException_if_regex_is_null()
        {
            var card = new ThumbnailCard();

            var sut = new ThumbnailCardAssertions(card);

            Action act = () => sut.TextMatching(null);

            act.ShouldThrow<ArgumentNullException>();
        }

        [Test]
        public void TextMatching_should_throw_ArgumentNullException_if_when_capturing_groups_regex_is_null()
        {
            IList<string> matches;

            var card = new ThumbnailCard();

            var sut = new ThumbnailCardAssertions(card);

            Action act = () => sut.TextMatching(null, "(.*)", out matches);

            act.ShouldThrow<ArgumentNullException>();
        }

        [Test]
        public void TextMatching_should_throw_ArgumentNullException_if_groupMatchRegex_is_null()
        {
            IList<string> matches;

            var card = new ThumbnailCard();

            var sut = new ThumbnailCardAssertions(card);

            Action act = () => sut.TextMatching("(.*)", null, out matches);

            act.ShouldThrow<ArgumentNullException>();
        }
    }
}
