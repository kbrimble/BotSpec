using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using KBrimble.DirectLineTester.Assertions.Cards;
using KBrimble.DirectLineTester.Assertions.Cards.CardComponents;
using KBrimble.DirectLineTester.Exceptions;
using KBrimble.DirectLineTester.Models.Cards;
using NUnit.Framework;

namespace KBrimble.DirectLineTester.Tests.Unit.CardAssertionTests.CardComponentAssertionTests.When_testing_facts
{
    [TestFixture]
    public class For_a_value_matching
    {
        [TestCase("some text")]
        [TestCase("")]
        [TestCase("symbols ([*])?")]
        public void HasValueMatching_should_pass_if_regex_exactly_matches_message_Value(string valueAndRegex)
        {
            var fact = new Fact(value: valueAndRegex);

            var sut = new FactAssertions(fact);

            Action act = () => sut.HasValueMatching(valueAndRegex);

            act.ShouldNotThrow<Exception>();
        }

        [TestCase("some text", "SOME TEXT")]
        [TestCase(@"SYMBOLS ([*])?", @"symbols ([*])?")]
        public void HasValueMatching_should_pass_regardless_of_case(string value, string regex)
        {
            var fact = new Fact(value: value);

            var sut = new FactAssertions(fact);

            Action act = () => sut.HasValueMatching(regex);

            act.ShouldNotThrow<Exception>();
        }

        [TestCase("some text", "so.*xt")]
        [TestCase("some text", "[a-z ]*")]
        [TestCase("some text", "s(ome tex)t")]
        public void HasValueMatching_should_pass_when_using_standard_regex_features(string value, string regex)
        {
            var fact = new Fact(value: value);

            var sut = new FactAssertions(fact);

            Action act = () => sut.HasValueMatching(regex);

            act.ShouldNotThrow<Exception>();
        }

        [TestCase("some text", "some text!")]
        [TestCase("some text", "^[j-z ]*$")]
        [TestCase("some text", "s{12}")]
        public void HasValueMatching_should_throw_FactAssertionFailedException_for_non_matching_regexes(string value, string regex)
        {
            var fact = new Fact(value: value);

            var sut = new FactAssertions(fact);

            Action act = () => sut.HasValueMatching(regex);

            act.ShouldThrow<FactAssertionFailedException>();
        }

        [Test]
        public void HasValueMatching_should_not_output_matches_when_regex_does_not_match_value()
        {
            IList<string> matches = null;

            var fact = new Fact(value: "some text");

            var sut = new FactAssertions(fact);

            Action act = () => sut.HasValueMatching("non matching regex", "(some text)", out matches);

            act.ShouldThrow<FactAssertionFailedException>();
            matches.Should().BeNull();
        }

        [Test]
        public void HasValueMatching_should_not_output_matches_when_groupMatchingRegex_does_not_match_value()
        {
            IList<string> matches;

            var fact = new Fact(value: "some text");

            var sut = new FactAssertions(fact);

            sut.HasValueMatching("some text", "(non matching)", out matches);

            matches.Should().BeNull();
        }

        [Test]
        public void HasValueMatching_should_output_matches_when_groupMatchingRegex_matches_value()
        {
            IList<string> matches;

            const string someText = "some text";
            var fact = new Fact(value: someText);

            var sut = new FactAssertions(fact);

            sut.HasValueMatching(someText, $"({someText})", out matches);

            matches.First().Should().Be(someText);
        }

        [Test]
        public void HasValueMatching_should_output_multiple_matches_when_groupMatchingRegex_matches_value_several_times()
        {
            IList<string> matches;

            const string someText = "some text";
            var fact = new Fact(value: someText);

            var sut = new FactAssertions(fact);

            const string match1 = "some";
            const string match2 = "text";
            sut.HasValueMatching(someText, $"({match1}) ({match2})", out matches);

            matches.Should().Contain(match1, match2);
        }

        [Test]
        public void HasValueMatching_should_throw_FactAssertionFailedException_when_value_is_null()
        {
            var card = new Fact();

            var sut = new FactAssertions(card);

            Action act = () => sut.HasValueMatching("anything");

            act.ShouldThrow<FactAssertionFailedException>();
        }

        [Test]
        public void HasValueMatching_should_throw_FactAssertionFailedException_when_trying_to_capture_groups_but_value_is_null()
        {
            IList<string> matches;
            var card = new Fact();

            var sut = new FactAssertions(card);

            Action act = () => sut.HasValueMatching("anything", "(.*)", out matches);

            act.ShouldThrow<FactAssertionFailedException>();
        }

        [Test]
        public void HasValueMatching_should_throw_ArgumentNullException_if_regex_is_null()
        {
            var card = new Fact();

            var sut = new FactAssertions(card);

            Action act = () => sut.HasValueMatching(null);

            act.ShouldThrow<ArgumentNullException>();
        }

        [Test]
        public void HasValueMatching_should_throw_ArgumentNullException_if_when_capturing_groups_regex_is_null()
        {
            IList<string> matches;

            var card = new Fact();

            var sut = new FactAssertions(card);

            Action act = () => sut.HasValueMatching(null, "(.*)", out matches);

            act.ShouldThrow<ArgumentNullException>();
        }

        [Test]
        public void HasValueMatching_should_throw_ArgumentNullException_if_groupMatchRegex_is_null()
        {
            IList<string> matches;

            var card = new Fact();

            var sut = new FactAssertions(card);

            Action act = () => sut.HasValueMatching("(.*)", null, out matches);

            act.ShouldThrow<ArgumentNullException>();
        }
    }
}
