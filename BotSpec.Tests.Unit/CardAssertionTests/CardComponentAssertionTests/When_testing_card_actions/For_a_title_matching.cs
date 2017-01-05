using System;
using System.Collections.Generic;
using System.Linq;
using BotSpec.Assertions.Cards.CardComponents;
using BotSpec.Exceptions;
using BotSpec.Models.Cards;
using FluentAssertions;
using NUnit.Framework;

namespace BotSpec.Tests.Unit.CardAssertionTests.CardComponentAssertionTests.When_testing_card_actions
{
    [TestFixture]
    public class For_a_title_matching
    {
        [Test]
        public void TitleMatching_should_throw_ArgumentNullException_when_regex_is_null()
        {
            var cardAction = new CardAction(title: "some text");

            var sut = new CardActionAssertions(cardAction);

            Action act = () => sut.TitleMatching(null);

            act.ShouldThrow<ArgumentNullException>();
        }

        [Test]
        public void TitleMatching_should_throw_ArgumentNullException_when_regex_is_null_when_matching_groups()
        {
            var cardAction = new CardAction(title: "some text");

            var sut = new CardActionAssertions(cardAction);

            IList<string> matches;
            Action act = () => sut.TitleMatching(null, "(.*)", out matches);

            act.ShouldThrow<ArgumentNullException>();
        }

        [Test]
        public void TitleMatching_should_throw_ArgumentNullException_when_groupMatchRegex_is_null()
        {
            IList<string> matches;

            var cardAction = new CardAction(title: "some text");

            var sut = new CardActionAssertions(cardAction);

            Action act = () => sut.TitleMatching(".*", null, out matches);

            act.ShouldThrow<ArgumentNullException>();
        }

        [TestCase("some text")]
        [TestCase("")]
        [TestCase("symbols ([*])?")]
        public void TitleMatching_should_pass_if_regex_exactly_matches_image_title(string titleAndRegex)
        {
            var cardAction = new CardAction(title: titleAndRegex);

            var sut = new CardActionAssertions(cardAction);

            Action act = () => sut.TitleMatching(titleAndRegex);

            act.ShouldNotThrow<Exception>();
        }

        [TestCase("some text", "SOME TEXT")]
        [TestCase("SYMBOLS ([*])?", "symbols ([*])?")]
        public void TitleMatching_should_pass_regardless_of_case(string title, string regex)
        {
            var cardAction = new CardAction(title: title);

            var sut = new CardActionAssertions(cardAction);

            Action act = () => sut.TitleMatching(regex);

            act.ShouldNotThrow<Exception>();
        }

        [TestCase("some text", "so.*xt")]
        [TestCase("some text", "[a-z ]*")]
        [TestCase("some text", "s(ome tex)t")]
        public void TitleMatching_should_pass_when_using_standard_regex_features(string title, string regex)
        {
            var cardAction = new CardAction(title: title);

            var sut = new CardActionAssertions(cardAction);

            Action act = () => sut.TitleMatching(regex);

            act.ShouldNotThrow<Exception>();
        }

        [TestCase("some text", "some text!")]
        [TestCase("some text", "^[j-z ]*$")]
        [TestCase("some text", "s{12}")]
        public void TitleMatching_should_throw_CardActionAssertionFailedException_for_non_matching_regexes(string title, string regex)
        {
            var cardAction = new CardAction(title: title);

            var sut = new CardActionAssertions(cardAction);

            Action act = () => sut.TitleMatching(regex);

            act.ShouldThrow<CardActionAssertionFailedException>();
        }

        [Test]
        public void TitleMatching_should_not_output_matches_when_regex_does_not_match_title()

        {
            IList<string> matches = null;

            var cardAction = new CardAction(title: "some text");

            var sut = new CardActionAssertions(cardAction);

            Action act = () => sut.TitleMatching("non matching regex", "(some text)", out matches);

            act.ShouldThrow<CardActionAssertionFailedException>();
            matches.Should().BeNull();
        }

        [Test]
        public void TitleMatching_should_not_output_matches_when_groupMatchingRegex_does_not_match_title()
        {
            IList<string> matches;

            var cardAction = new CardAction(title: "some text");

            var sut = new CardActionAssertions(cardAction);

            sut.TitleMatching("some text", "(non matching)", out matches);

            matches.Should().BeNull();
        }

        [Test]
        public void TitleMatching_should_output_matches_when_groupMatchingRegex_matches_title()
        {
            IList<string> matches;

            const string someText = "some text";
            var cardAction = new CardAction(title: someText);

            var sut = new CardActionAssertions(cardAction);

            sut.TitleMatching(someText, $"({someText})", out matches);

            matches.First().Should().Be(someText);
        }

        [Test]
        public void TitleMatching_should_output_multiple_matches_when_groupMatchingRegex_matches_title_several_times()
        {
            IList<string> matches;

            const string someText = "some text";
            var cardAction = new CardAction(title: someText);

            var sut = new CardActionAssertions(cardAction);

            var match1 = "some";
            var match2 = "text";
            sut.TitleMatching(someText, $"({match1}) ({match2})", out matches);

            matches.Should().Contain(match1, match2);
        }

        [Test]
        public void TitleMatching_should_throw_CardActionAssertionFailedException_when_title_is_null()
        {
            var cardAction = new CardAction();

            var sut = new CardActionAssertions(cardAction);

            Action act = () => sut.TitleMatching("anything");

            act.ShouldThrow<CardActionAssertionFailedException>();
        }

        [Test]
        public void TitleMatching_should_throw_CardActionAssertionFailedException_when_trying_to_capture_groups_but_title_is_null()
        {
            IList<string> matches;
            var cardAction = new CardAction();

            var sut = new CardActionAssertions(cardAction);

            Action act = () => sut.TitleMatching("anything", "(.*)", out matches);

            act.ShouldThrow<CardActionAssertionFailedException>();
        }
    }
}
