using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using KBrimble.DirectLineTester.Assertions.Cards;
using KBrimble.DirectLineTester.Exceptions;
using KBrimble.DirectLineTester.Models.Cards;
using NUnit.Framework;

namespace KBrimble.DirectLineTester.Tests.Unit.CardAssertionTests.When_testing_groups_of_thumbnail_card_sets
{
    [TestFixture]
    public class For_a_title_matching
    {
        [TestCase("some text")]
        [TestCase("")]
        [TestCase("symbols ([*])?")]
        public void HasTitleMatching_should_pass_if_regex_exactly_matches_message_Title_of_one_card_in_any_set(string cardTitleAndRegex)
        {
            var cards = ThumbnailCardSetTestData.CreateGroupOfThumbnailCardSetWithOneCardThatHasSetProperties(title: cardTitleAndRegex).ToList();

            var sut = new GroupOfThumbnailCardSetAssertions(cards);

            Action act = () => sut.HasTitleMatching(cardTitleAndRegex);

            act.ShouldNotThrow<Exception>();
        }

        [TestCase("some text", "SOME TEXT")]
        [TestCase(@"SYMBOLS ([*])?", @"symbols ([*])?")]
        public void HasTitleMatching__should_pass_if_regex_exactly_matches_Title_of_at_least_1_card_regardless_of_case_in_any_set(string cardTitle, string regex)
        {
            var cards = ThumbnailCardSetTestData.CreateGroupOfThumbnailCardSetWithOneCardThatHasSetProperties(title: cardTitle).ToList();

            var sut = new GroupOfThumbnailCardSetAssertions(cards);

            Action act = () => sut.HasTitleMatching(regex);

            act.ShouldNotThrow<Exception>();
        }

        [TestCase("some text", "so.*xt")]
        [TestCase("some text", "[a-z ]*")]
        [TestCase("some text", "s(ome tex)t")]
        public void HasTitleMatching_should_pass_when_using_standard_regex_features(string cardTitle, string regex)
        {
            var cards = ThumbnailCardSetTestData.CreateGroupOfThumbnailCardSetWithOneCardThatHasSetProperties(title: cardTitle).ToList();

            var sut = new GroupOfThumbnailCardSetAssertions(cards);

            Action act = () => sut.HasTitleMatching(regex);

            act.ShouldNotThrow<Exception>();
        }

        [TestCase("some text", "some text!")]
        [TestCase("some text", "^[j-z ]*$")]
        [TestCase("some text", "s{12}")]
        public void HasTitleMatching_should_throw_GroupOfThumbnailCardSetAssertionFailedException_when_regex_matches_no_cards_in_any_set(string cardTitle, string regex)
        {
            var cards = ThumbnailCardSetTestData.CreateGroupOfThumbnailCardSetWithOneCardThatHasSetProperties(title: cardTitle).ToList();

            var sut = new GroupOfThumbnailCardSetAssertions(cards);

            Action act = () => sut.HasTitleMatching(regex);

            act.ShouldThrow<GroupOfThumbnailCardSetAssertionFailedException>();
        }

        [Test]
        public void HasTitleMatching_should_throw_GroupOfThumbnailCardSetAssertionFailedException_when_Title_of_all_cards_is_null_in_every_set()
        {
            var cards = ThumbnailCardSetTestData.CreateEmptyGroupOfThumbnailCardSets().ToList();

            var sut = new GroupOfThumbnailCardSetAssertions(cards);

            Action act = () => sut.HasTitleMatching(".*");

            act.ShouldThrow<GroupOfThumbnailCardSetAssertionFailedException>();
        }

        [Test]
        public void HasTitleMatching_should_throw_GroupOfThumbnailCardSetAssertionFailedException_when_trying_to_capture_groups_but_Title_of_all_cards_is_null()
        {
            IList<string> matches;

            var cards = ThumbnailCardSetTestData.CreateEmptyGroupOfThumbnailCardSets().ToList();

            var sut = new GroupOfThumbnailCardSetAssertions(cards);

            Action act = () => sut.HasTitleMatching(".*", "(.*)", out matches);

            act.ShouldThrow<GroupOfThumbnailCardSetAssertionFailedException>();
        }

        [Test]
        public void HasTitleMatching_should_not_output_matches_when_regex_does_not_match_Title_of_any_cards_in_any_set()
        {
            IList<string> matches = null;

            var cards = ThumbnailCardSetTestData.CreateRandomGroupOfThumbnailCardSets().ToList();

            var sut = new GroupOfThumbnailCardSetAssertions(cards);

            Action act = () => sut.HasTitleMatching("non matching regex", "(some text)", out matches);

            act.ShouldThrow<GroupOfThumbnailCardSetAssertionFailedException>();
            matches.Should().BeNull();
        }

        [Test]
        public void HasTitleMatching_should_not_output_matches_when_groupMatchingRegex_does_not_match_Title_of_any_card_in_any_set()
        {
            IList<string> matches = null;

            var cards = ThumbnailCardSetTestData.CreateRandomGroupOfThumbnailCardSets().ToList();

            var sut = new GroupOfThumbnailCardSetAssertions(cards);

            sut.HasTitleMatching(".*", "(non matching)", out matches);

            matches.Should().BeNull();
        }

        [Test]
        public void HasTitleMatching_should_output_matches_when_groupMatchingRegex_matches_Title_of_any_card_in_any_set()
        {
            IList<string> matches;

            var cards = ThumbnailCardSetTestData.CreateGroupOfThumbnailCardSetWithOneCardThatHasSetProperties(title: "some text").ToList();

            var sut = new GroupOfThumbnailCardSetAssertions(cards);

            sut.HasTitleMatching(".*", "(some text)", out matches);

            matches.First().Should().Be("some text");
        }

        [Test]
        public void HasTitleMatching_should_output_multiple_matches_when_groupMatchingRegex_matches_Title_on_multiple_cards()
        {
            IList<string> matches;

            var cards = ThumbnailCardSetTestData.CreateRandomGroupOfThumbnailCardSets().ToList();
            var matchingCard1 = new ThumbnailCard(title: "some text");
            var matchingCard2 = new ThumbnailCard(title: "same text");
            var matchingAssertionSet = new ThumbnailCardSetAssertions(new[] { matchingCard1, matchingCard2 });
            cards.Add(matchingAssertionSet);

            var sut = new GroupOfThumbnailCardSetAssertions(cards);

            sut.HasTitleMatching(".*", @"(s[oa]me) (text)", out matches);

            matches.Should().Contain("some", "same", "text");
        }

        [Test]
        public void HasTitleMatching_should_throw_ArgumentNullException_if_regex_is_null()
        {
            var cards = ThumbnailCardSetTestData.CreateRandomGroupOfThumbnailCardSets().ToList();

            var sut = new GroupOfThumbnailCardSetAssertions(cards);

            Action act = () => sut.HasTitleMatching(null);

            act.ShouldThrow<ArgumentNullException>();
        }

        [Test]
        public void HasTitleMatching_should_throw_ArgumentNullException_if_when_capturing_groups_regex_is_null()
        {
            IList<string> matches;

            var cards = ThumbnailCardSetTestData.CreateRandomGroupOfThumbnailCardSets().ToList();

            var sut = new GroupOfThumbnailCardSetAssertions(cards);

            Action act = () => sut.HasTitleMatching(null, "(.*)", out matches);

            act.ShouldThrow<ArgumentNullException>();
        }

        [Test]
        public void HasTitleMatching_should_throw_ArgumentNullException_if_groupMatchRegex_is_null()
        {
            IList<string> matches;

            var cards = ThumbnailCardSetTestData.CreateRandomGroupOfThumbnailCardSets().ToList();

            var sut = new GroupOfThumbnailCardSetAssertions(cards);

            Action act = () => sut.HasTitleMatching("(.*)", null, out matches);

            act.ShouldThrow<ArgumentNullException>();
        }
    }
}