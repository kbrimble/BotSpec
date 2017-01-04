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
    public class For_a_key_matching
    {
        [TestCase("some text")]
        [TestCase("")]
        [TestCase("symbols ([*])?")]
        public void KeyMatching_should_pass_if_regex_exactly_matches_message_Key(string keyAndRegex)
        {
            var fact = new Fact(key: keyAndRegex);

            var sut = new FactAssertions(fact);

            Action act = () => sut.KeyMatching(keyAndRegex);

            act.ShouldNotThrow<Exception>();
        }

        [TestCase("some text", "SOME TEXT")]
        [TestCase(@"SYMBOLS ([*])?", @"symbols ([*])?")]
        public void KeyMatching_should_pass_regardless_of_case(string key, string regex)
        {
            var fact = new Fact(key: key);

            var sut = new FactAssertions(fact);

            Action act = () => sut.KeyMatching(regex);

            act.ShouldNotThrow<Exception>();
        }

        [TestCase("some text", "so.*xt")]
        [TestCase("some text", "[a-z ]*")]
        [TestCase("some text", "s(ome tex)t")]
        public void KeyMatching_should_pass_when_using_standard_regex_features(string key, string regex)
        {
            var fact = new Fact(key: key);

            var sut = new FactAssertions(fact);

            Action act = () => sut.KeyMatching(regex);

            act.ShouldNotThrow<Exception>();
        }

        [TestCase("some text", "some text!")]
        [TestCase("some text", "^[j-z ]*$")]
        [TestCase("some text", "s{12}")]
        public void KeyMatching_should_throw_FactAssertionFailedException_for_non_matching_regexes(string key, string regex)
        {
            var fact = new Fact(key: key);

            var sut = new FactAssertions(fact);

            Action act = () => sut.KeyMatching(regex);

            act.ShouldThrow<FactAssertionFailedException>();
        }

        [Test]
        public void KeyMatching_should_not_output_matches_when_regex_does_not_match_key()
        {
            IList<string> matches = null;

            var fact = new Fact(key: "some text");

            var sut = new FactAssertions(fact);

            Action act = () => sut.KeyMatching("non matching regex", "(some text)", out matches);

            act.ShouldThrow<FactAssertionFailedException>();
            matches.Should().BeNull();
        }

        [Test]
        public void KeyMatching_should_not_output_matches_when_groupMatchingRegex_does_not_match_key()
        {
            IList<string> matches;

            var fact = new Fact(key: "some text");

            var sut = new FactAssertions(fact);

            sut.KeyMatching("some text", "(non matching)", out matches);

            matches.Should().BeNull();
        }

        [Test]
        public void KeyMatching_should_output_matches_when_groupMatchingRegex_matches_key()
        {
            IList<string> matches;

            const string someText = "some text";
            var fact = new Fact(key: someText);

            var sut = new FactAssertions(fact);

            sut.KeyMatching(someText, $"({someText})", out matches);

            matches.First().Should().Be(someText);
        }

        [Test]
        public void KeyMatching_should_output_multiple_matches_when_groupMatchingRegex_matches_key_several_times()
        {
            IList<string> matches;

            const string someText = "some text";
            var fact = new Fact(key: someText);

            var sut = new FactAssertions(fact);

            const string match1 = "some";
            const string match2 = "text";
            sut.KeyMatching(someText, $"({match1}) ({match2})", out matches);

            matches.Should().Contain(match1, match2);
        }

        [Test]
        public void KeyMatching_should_throw_FactAssertionFailedException_when_key_is_null()
        {
            var card = new Fact();

            var sut = new FactAssertions(card);

            Action act = () => sut.KeyMatching("anything");

            act.ShouldThrow<FactAssertionFailedException>();
        }

        [Test]
        public void KeyMatching_should_throw_FactAssertionFailedException_when_trying_to_capture_groups_but_key_is_null()
        {
            IList<string> matches;
            var card = new Fact();

            var sut = new FactAssertions(card);

            Action act = () => sut.KeyMatching("anything", "(.*)", out matches);

            act.ShouldThrow<FactAssertionFailedException>();
        }

        [Test]
        public void KeyMatching_should_throw_ArgumentNullException_if_regex_is_null()
        {
            var card = new Fact();

            var sut = new FactAssertions(card);

            Action act = () => sut.KeyMatching(null);

            act.ShouldThrow<ArgumentNullException>();
        }

        [Test]
        public void KeyMatching_should_throw_ArgumentNullException_if_when_capturing_groups_regex_is_null()
        {
            IList<string> matches;

            var card = new Fact();

            var sut = new FactAssertions(card);

            Action act = () => sut.KeyMatching(null, "(.*)", out matches);

            act.ShouldThrow<ArgumentNullException>();
        }

        [Test]
        public void KeyMatching_should_throw_ArgumentNullException_if_groupMatchRegex_is_null()
        {
            IList<string> matches;

            var card = new Fact();

            var sut = new FactAssertions(card);

            Action act = () => sut.KeyMatching("(.*)", null, out matches);

            act.ShouldThrow<ArgumentNullException>();
        }
    }
}
