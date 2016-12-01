using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using KBrimble.DirectLineTester.Assertions.Cards;
using KBrimble.DirectLineTester.Exceptions;
using KBrimble.DirectLineTester.Models.Cards;
using KBrimble.DirectLineTester.Tests.Unit.CardAssertionTests.When_testing_card_image_sets;
using NUnit.Framework;

namespace KBrimble.DirectLineTester.Tests.Unit.CardAssertionTests.When_testing_card_action_sets
{
    [TestFixture]
    public class For_a_type_matching
    {
        [TestCase("some text")]
        [TestCase("")]
        [TestCase("symbols ([*])?")]
        public void HasTypeMatching_should_pass_if_regex_exactly_matches_message_Type_of_one_card(string cardTypeAndRegex)
        {
            var cardActions = CardActionTestData.CreateCardActionSetWithOneMessageThatHasSetProperties(type: cardTypeAndRegex);

            var sut = new CardActionSetAssertions(cardActions);

            Action act = () => sut.HasTypeMatching(cardTypeAndRegex);

            act.ShouldNotThrow<Exception>();
        }

        [TestCase("some text", "SOME TEXT")]
        [TestCase(@"SYMBOLS ([*])?", @"symbols ([*])?")]
        public void HasTypeMatching_should_pass_if_regex_exactly_matches_Type_of_at_least_1_card_regardless_of_case(string type, string regex)
        {
            var cardActions = CardActionTestData.CreateCardActionSetWithOneMessageThatHasSetProperties(type: type);

            var sut = new CardActionSetAssertions(cardActions);

            Action act = () => sut.HasTypeMatching(regex);

            act.ShouldNotThrow<Exception>();
        }

        [TestCase("some text", "so.*xt")]
        [TestCase("some text", "[a-z ]*")]
        [TestCase("some text", "s(ome tex)t")]
        public void HasTypeMatching_should_pass_when_using_standard_regex_features(string type, string regex)
        {
            var cardActions = CardActionTestData.CreateCardActionSetWithOneMessageThatHasSetProperties(type: type);

            var sut = new CardActionSetAssertions(cardActions);

            Action act = () => sut.HasTypeMatching(regex);

            act.ShouldNotThrow<Exception>();
        }

        [TestCase("some text!")]
        [TestCase("^[j-z ]*$")]
        [TestCase("s{12}")]
        public void HasTypeMatching_should_throw_CardActionSetAssertionFailedException_when_regex_matches_no_cards(string regex)
        {
            var cardActions = CardActionTestData.CreateRandomCardActions();

            var sut = new CardActionSetAssertions(cardActions);

            Action act = () => sut.HasTypeMatching(regex);

            act.ShouldThrow<CardActionSetAssertionFailedException>();
        }

        [Test]
        public void HasTypeMatching_should_throw_CardActionSetAssertionFailedException_when_Type_of_all_cards_is_null()
        {
            var cardActions = Enumerable.Range(1, 5).Select(_ => new CardAction()).ToList();

            var sut = new CardActionSetAssertions(cardActions);

            Action act = () => sut.HasTypeMatching(".*");

            act.ShouldThrow<CardActionSetAssertionFailedException>();
        }

        [Test]
        public void HasTypeMatching_should_throw_CardActionSetAssertionFailedException_when_trying_to_capture_groups_but_Type_of_all_cards_is_null()
        {
            IList<string> matches;

            var cardActions = Enumerable.Range(1, 5).Select(_ => new CardAction()).ToList();

            var sut = new CardActionSetAssertions(cardActions);

            Action act = () => sut.HasTypeMatching(".*", "(.*)", out matches);

            act.ShouldThrow<CardActionSetAssertionFailedException>();
        }

        [Test]
        public void HasTypeMatching_should_not_output_matches_when_regex_does_not_match_Type_of_any_cards()
        {
            IList<string> matches = null;

            var cardActions = CardActionTestData.CreateRandomCardActions();

            var sut = new CardActionSetAssertions(cardActions);

            Action act = () => sut.HasTypeMatching("non matching regex", "(some text)", out matches);

            act.ShouldThrow<CardActionSetAssertionFailedException>();
            matches.Should().BeNull();
        }

        [Test]
        public void HasTypeMatching_should_not_output_matches_when_groupMatchingRegex_does_not_match_Type_of_any_card()
        {
            IList<string> matches;

            var cardActions = CardActionTestData.CreateRandomCardActions();

            var sut = new CardActionSetAssertions(cardActions);

            sut.HasTypeMatching(".*", "(non matching)", out matches);

            matches.Should().BeNull();
        }

        [Test]
        public void HasTypeMatching_should_output_matches_when_groupMatchingRegex_matches_Type_of_any_card()
        {
            IList<string> matches;

            const string someType = "some text";
            var cardActions = CardActionTestData.CreateCardActionSetWithOneMessageThatHasSetProperties(type: someType);

            var sut = new CardActionSetAssertions(cardActions);

            sut.HasTypeMatching(someType, $"({someType})", out matches);

            matches.First().Should().Be(someType);
        }

        [Test]
        public void HasTypeMatching_should_output_multiple_matches_when_groupMatchingRegex_matches_Type_several_times_for_a_single_card()
        {
            IList<string> matches;

            const string someType = "some text";
            var cardActions = CardActionTestData.CreateCardActionSetWithOneMessageThatHasSetProperties(type: someType);

            var sut = new CardActionSetAssertions(cardActions);

            const string match1 = "some";
            const string match2 = "text";
            sut.HasTypeMatching(someType, $"({match1}) ({match2})", out matches);

            matches.Should().Contain(match1, match2);
        }

        [Test]
        public void HasTypeMatching_should_output_multiple_matches_when_groupMatchingRegex_matches_Type_on_multiple_cards()
        {
            IList<string> matches;

            var cardActions = CardActionTestData.CreateRandomCardActions();
            cardActions.Add(new CardAction(type: "some text"));
            cardActions.Add(new CardAction(type: "same text"));

            var sut = new CardActionSetAssertions(cardActions);

            sut.HasTypeMatching(".*", @"(s[oa]me) (text)", out matches);

            matches.Should().Contain("some", "same", "text");
        }

        [Test]
        public void HasTypeMatching_should_throw_ArgumentNullException_if_regex_is_null()
        {
            var cardActions = CardActionTestData.CreateRandomCardActions();

            var sut = new CardActionSetAssertions(cardActions);

            Action act = () => sut.HasTypeMatching(null);

            act.ShouldThrow<ArgumentNullException>();
        }

        [Test]
        public void HasTypeMatching_should_throw_ArgumentNullException_when_capturing_groups_if_regex_is_null()
        {
            IList<string> matches;

            var cardActions = CardActionTestData.CreateRandomCardActions();

            var sut = new CardActionSetAssertions(cardActions);

            Action act = () => sut.HasTypeMatching(null, "(.*)", out matches);

            act.ShouldThrow<ArgumentNullException>();
        }

        [Test]
        public void HasTypeMatching_should_throw_ArgumentNullException_if_groupMatchRegex_is_null()
        {
            IList<string> matches;

            var cardActions = CardActionTestData.CreateRandomCardActions();

            var sut = new CardActionSetAssertions(cardActions);

            Action act = () => sut.HasTypeMatching("(.*)", null, out matches);

            act.ShouldThrow<ArgumentNullException>();
        }

    }
}
