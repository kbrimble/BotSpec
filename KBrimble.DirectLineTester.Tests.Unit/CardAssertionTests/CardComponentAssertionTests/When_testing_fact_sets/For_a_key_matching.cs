using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using KBrimble.DirectLineTester.Assertions.Cards.CardComponents;
using KBrimble.DirectLineTester.Exceptions;
using KBrimble.DirectLineTester.Models.Cards;
using KBrimble.DirectLineTester.Tests.Unit.TestData;
using NUnit.Framework;

namespace KBrimble.DirectLineTester.Tests.Unit.CardAssertionTests.CardComponentAssertionTests.When_testing_fact_sets
{
    [TestFixture]
    public class For_a_key_matching
    {
        [TestCase("some text")]
        [TestCase("")]
        [TestCase("symbols ([*])?")]
        public void HasKeyMatching_should_pass_if_regex_exactly_matches_message_Key_of_one_card(string keyAndRegex)
        {
            var facts = FactTestData.CreateFactSetWithOneFactThatHasSetProperties(key: keyAndRegex);

            var sut = new FactSetAssertions(facts);

            Action act = () => sut.HasKeyMatching(keyAndRegex);

            act.ShouldNotThrow<Exception>();
        }

        [TestCase("some text", "SOME TEXT")]
        [TestCase(@"SYMBOLS ([*])?", @"symbols ([*])?")]
        public void HasKeyMatching_should_pass_if_regex_exactly_matches_Key_of_at_least_1_card_regardless_of_case(string key, string regex)
        {
            var facts = FactTestData.CreateFactSetWithOneFactThatHasSetProperties(key: key);

            var sut = new FactSetAssertions(facts);

            Action act = () => sut.HasKeyMatching(regex);

            act.ShouldNotThrow<Exception>();
        }

        [TestCase("some text", "so.*xt")]
        [TestCase("some text", "[a-z ]*")]
        [TestCase("some text", "s(ome tex)t")]
        public void HasKeyMatching_should_pass_when_using_standard_regex_features(string key, string regex)
        {
            var facts = FactTestData.CreateFactSetWithOneFactThatHasSetProperties(key: key);

            var sut = new FactSetAssertions(facts);

            Action act = () => sut.HasKeyMatching(regex);

            act.ShouldNotThrow<Exception>();
        }

        [TestCase("some text!")]
        [TestCase("^[j-z ]*$")]
        [TestCase("s{12}")]
        public void HasKeyMatching_should_throw_FactAssertionFailedException_when_regex_matches_no_cards(string regex)
        {
            var facts = FactTestData.CreateRandomFacts();

            var sut = new FactSetAssertions(facts);

            Action act = () => sut.HasKeyMatching(regex);

            act.ShouldThrow<FactAssertionFailedException>();
        }

        [Test]
        public void HasKeyMatching_should_throw_FactAssertionFailedException_when_Key_of_all_cards_is_null()
        {
            var facts = Enumerable.Range(1, 5).Select(_ => new Fact()).ToList();

            var sut = new FactSetAssertions(facts);

            Action act = () => sut.HasKeyMatching(".*");

            act.ShouldThrow<FactAssertionFailedException>();
        }

        [Test]
        public void HasKeyMatching_should_throw_FactAssertionFailedException_when_trying_to_capture_groups_but_Key_of_all_cards_is_null()
        {
            IList<string> matches;

            var facts = Enumerable.Range(1, 5).Select(_ => new Fact()).ToList();

            var sut = new FactSetAssertions(facts);

            Action act = () => sut.HasKeyMatching(".*", "(.*)", out matches);

            act.ShouldThrow<FactAssertionFailedException>();
        }

        [Test]
        public void HasKeyMatching_should_not_output_matches_when_regex_does_not_match_Key_of_any_cards()
        {
            IList<string> matches = null;

            var facts = FactTestData.CreateRandomFacts();

            var sut = new FactSetAssertions(facts);

            Action act = () => sut.HasKeyMatching("non matching regex", "(some text)", out matches);

            act.ShouldThrow<FactAssertionFailedException>();
            matches.Should().BeNull();
        }

        [Test]
        public void HasKeyMatching_should_not_output_matches_when_groupMatchingRegex_does_not_match_Key_of_any_card()
        {
            IList<string> matches;

            var facts = FactTestData.CreateRandomFacts();

            var sut = new FactSetAssertions(facts);

            sut.HasKeyMatching(".*", "(non matching)", out matches);

            matches.Should().BeNull();
        }

        [Test]
        public void HasKeyMatching_should_output_matches_when_groupMatchingRegex_matches_Key_of_any_card()
        {
            IList<string> matches;

            const string someKey = "some text";
            var facts = FactTestData.CreateFactSetWithOneFactThatHasSetProperties(key: someKey);

            var sut = new FactSetAssertions(facts);

            sut.HasKeyMatching(someKey, $"({someKey})", out matches);

            matches.First().Should().Be(someKey);
        }

        [Test]
        public void HasKeyMatching_should_output_multiple_matches_when_groupMatchingRegex_matches_Key_several_times_for_a_single_card()
        {
            IList<string> matches;

            const string someKey = "some text";
            var facts = FactTestData.CreateFactSetWithOneFactThatHasSetProperties(key: someKey);

            var sut = new FactSetAssertions(facts);

            const string match1 = "some";
            const string match2 = "text";
            sut.HasKeyMatching(someKey, $"({match1}) ({match2})", out matches);

            matches.Should().Contain(match1, match2);
        }

        [Test]
        public void HasKeyMatching_should_output_multiple_matches_when_groupMatchingRegex_matches_Key_on_multiple_cards()
        {
            IList<string> matches;

            var facts = FactTestData.CreateRandomFacts();
            facts.Add(new Fact(key: "some text"));
            facts.Add(new Fact(key: "same text"));

            var sut = new FactSetAssertions(facts);

            sut.HasKeyMatching(".*", @"(s[oa]me) (text)", out matches);

            matches.Should().Contain("some", "same", "text");
        }

        [Test]
        public void HasKeyMatching_should_throw_ArgumentNullException_if_regex_is_null()
        {
            var facts = FactTestData.CreateRandomFacts();

            var sut = new FactSetAssertions(facts);

            Action act = () => sut.HasKeyMatching(null);

            act.ShouldThrow<ArgumentNullException>();
        }

        [Test]
        public void HasKeyMatching_should_throw_ArgumentNullException_when_capturing_groups_if_regex_is_null()
        {
            IList<string> matches;

            var facts = FactTestData.CreateRandomFacts();

            var sut = new FactSetAssertions(facts);

            Action act = () => sut.HasKeyMatching(null, "(.*)", out matches);

            act.ShouldThrow<ArgumentNullException>();
        }

        [Test]
        public void HasKeyMatching_should_throw_ArgumentNullException_if_groupMatchRegex_is_null()
        {
            IList<string> matches;

            var facts = FactTestData.CreateRandomFacts();

            var sut = new FactSetAssertions(facts);

            Action act = () => sut.HasKeyMatching("(.*)", null, out matches);

            act.ShouldThrow<ArgumentNullException>();
        }
    }
}
