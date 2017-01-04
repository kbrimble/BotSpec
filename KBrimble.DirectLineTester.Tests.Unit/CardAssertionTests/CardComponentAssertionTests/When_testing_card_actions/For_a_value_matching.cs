using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using KBrimble.DirectLineTester.Assertions.Cards.CardComponents;
using KBrimble.DirectLineTester.Exceptions;
using KBrimble.DirectLineTester.Models.Cards;
using NUnit.Framework;

namespace KBrimble.DirectLineTester.Tests.Unit.CardAssertionTests.CardComponentAssertionTests.When_testing_card_actions
{
    [TestFixture]
    public class For_a_value_matching
    {
        [Test]
        public void ValueMatching_should_throw_ArgumentNullException_when_regex_is_null()
        {
            var cardAction = new CardAction(value: "some text");

            var sut = new CardActionAssertions(cardAction);

            Action act = () => sut.ValueMatching(null);

            act.ShouldThrow<ArgumentNullException>();
        }

        [Test]
        public void ValueMatching_should_throw_ArgumentNullException_when_regex_is_null_when_trying_to_match_groups()
        {
            var cardAction = new CardAction(value: "some text");

            var sut = new CardActionAssertions(cardAction);

            IList<string> matches;
            Action act = () => sut.ValueMatching(null, "(.*)", out matches);

            act.ShouldThrow<ArgumentNullException>();
        }

        [Test]
        public void ValueMatching_should_throw_ArgumentNullException_when_groupMatchRegex_is_null()
        {
            IList<string> matches;

            var cardAction = new CardAction(value: "some text");

            var sut = new CardActionAssertions(cardAction);

            Action act = () => sut.ValueMatching(".*", null, out matches);

            act.ShouldThrow<ArgumentNullException>();
        }

        [TestCase("some text")]
        [TestCase("")]
        [TestCase("symbols ([*])?")]
        public void ValueMatching_should_pass_if_regex_exactly_matches_image_value(string valueAndRegex)
        {
            var cardAction = new CardAction(value: valueAndRegex);

            var sut = new CardActionAssertions(cardAction);

            Action act = () => sut.ValueMatching(valueAndRegex);

            act.ShouldNotThrow<Exception>();
        }

        [TestCase("some text", "SOME TEXT")]
        [TestCase("SYMBOLS ([*])?", "symbols ([*])?")]
        public void ValueMatching_should_pass_regardless_of_case(string value, string regex)
        {
            var cardAction = new CardAction(value: value);

            var sut = new CardActionAssertions(cardAction);

            Action act = () => sut.ValueMatching(regex);

            act.ShouldNotThrow<Exception>();
        }

        [TestCase("some text", "so.*xt")]
        [TestCase("some text", "[a-z ]*")]
        [TestCase("some text", "s(ome tex)t")]
        public void ValueMatching_should_pass_when_using_standard_regex_features(string value, string regex)
        {
            var cardAction = new CardAction(value: value);

            var sut = new CardActionAssertions(cardAction);

            Action act = () => sut.ValueMatching(regex);

            act.ShouldNotThrow<Exception>();
        }

        [TestCase("some text", "some text!")]
        [TestCase("some text", "^[j-z ]*$")]
        [TestCase("some text", "s{12}")]
        public void ValueMatching_should_throw_CardActionAssertionFailedException_for_non_matching_regexes(string value, string regex)
        {
            var cardAction = new CardAction(value: value);

            var sut = new CardActionAssertions(cardAction);

            Action act = () => sut.ValueMatching(regex);

            act.ShouldThrow<CardActionAssertionFailedException>();
        }

        [Test]
        public void ValueMatching_should_not_output_matches_when_regex_does_not_match_text()
        {
            IList<string> matches = null;

            var cardAction = new CardAction(value: "some text");

            var sut = new CardActionAssertions(cardAction);

            Action act = () => sut.ValueMatching("non matching regex", "(some text)", out matches);

            act.ShouldThrow<CardActionAssertionFailedException>();
            matches.Should().BeNull();
        }

        [Test]
        public void ValueMatching_should_not_output_matches_when_groupMatchingRegex_does_not_match_text()
        {
            IList<string> matches;

            var cardAction = new CardAction(value: "some text");

            var sut = new CardActionAssertions(cardAction);

            sut.ValueMatching("some text", "(non matching)", out matches);

            matches.Should().BeNull();
        }

        [Test]
        public void ValueMatching_should_output_matches_when_groupMatchingRegex_matches_text()
        {
            IList<string> matches;

            const string someText = "some text";
            var cardAction = new CardAction(value: someText);

            var sut = new CardActionAssertions(cardAction);

            sut.ValueMatching(someText, $"({someText})", out matches);

            matches.First().Should().Be(someText);
        }

        [Test]
        public void ValueMatching_should_output_multiple_matches_when_groupMatchingRegex_matches_text_several_times()
        {
            IList<string> matches;

            const string someText = "some text";
            var cardAction = new CardAction(value: someText);

            var sut = new CardActionAssertions(cardAction);

            var match1 = "some";
            var match2 = "text";
            sut.ValueMatching(someText, $"({match1}) ({match2})", out matches);

            matches.Should().Contain(match1, match2);
        }

        [Test]
        public void ValueMatching_should_throw_CardActionAssertionFailedException_when_text_is_null()
        {
            var cardAction = new CardAction();

            var sut = new CardActionAssertions(cardAction);

            Action act = () => sut.ValueMatching("anything");

            act.ShouldThrow<CardActionAssertionFailedException>();
        }

        [Test]
        public void ValueMatching_should_throw_CardActionAssertionFailedException_when_trying_to_capture_groups_but_text_is_null()
        {
            IList<string> matches;
            var cardAction = new CardAction();

            var sut = new CardActionAssertions(cardAction);

            Action act = () => sut.ValueMatching("anything", "(.*)", out matches);

            act.ShouldThrow<CardActionAssertionFailedException>();
        }
    }
}