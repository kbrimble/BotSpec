using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using KBrimble.DirectLineTester.Assertions.Cards;
using KBrimble.DirectLineTester.Exceptions;
using KBrimble.DirectLineTester.Models.Cards;
using NUnit.Framework;

namespace KBrimble.DirectLineTester.Tests.Unit.CardAssertionTests.When_testing_card_image_sets
{
    [TestFixture]
    public class For_a_url_matching
    {
        [TestCase("some text")]
        [TestCase("")]
        [TestCase("symbols ([*])?")]
        public void HasUrlMatching_should_pass_if_regex_exactly_matches_message_Url_of_one_card(string cardUrlAndRegex)
        {
            var cardImages = CardImageTestData.CreateCardImageSetWithOneMessageThatHasSetProperties(url: cardUrlAndRegex);

            var sut = new CardImageSetAssertions(cardImages);

            Action act = () => sut.HasUrlMatching(cardUrlAndRegex);

            act.ShouldNotThrow<Exception>();
        }

        [TestCase("some text", "SOME TEXT")]
        [TestCase(@"SYMBOLS ([*])?", @"symbols ([*])?")]
        public void HasUrlMatching_should_pass_if_regex_exactly_matches_Url_of_at_least_1_card_regardless_of_case(string url, string regex)
        {
            var cardImages = CardImageTestData.CreateCardImageSetWithOneMessageThatHasSetProperties(url: url);

            var sut = new CardImageSetAssertions(cardImages);

            Action act = () => sut.HasUrlMatching(regex);

            act.ShouldNotThrow<Exception>();
        }

        [TestCase("some text", "so.*xt")]
        [TestCase("some text", "[a-z ]*")]
        [TestCase("some text", "s(ome tex)t")]
        public void HasUrlMatching_should_pass_when_using_standard_regex_features(string url, string regex)
        {
            var cardImages = CardImageTestData.CreateCardImageSetWithOneMessageThatHasSetProperties(url: url);

            var sut = new CardImageSetAssertions(cardImages);

            Action act = () => sut.HasUrlMatching(regex);

            act.ShouldNotThrow<Exception>();
        }

        [TestCase("some text!")]
        [TestCase("^[j-z ]*$")]
        [TestCase("s{12}")]
        public void HasUrlMatching_should_throw_CardImageSetAssertionFailedException_when_regex_matches_no_cards(string regex)
        {
            var cardImages = CardImageTestData.CreateRandomCardImages();

            var sut = new CardImageSetAssertions(cardImages);

            Action act = () => sut.HasUrlMatching(regex);

            act.ShouldThrow<CardImageSetAssertionFailedException>();
        }

        [Test]
        public void HasUrlMatching_should_throw_CardImageSetAssertionFailedException_when_Url_of_all_cards_is_null()
        {
            var cardImages = Enumerable.Range(1, 5).Select(_ => new CardImage()).ToList();

            var sut = new CardImageSetAssertions(cardImages);

            Action act = () => sut.HasUrlMatching(".*");

            act.ShouldThrow<CardImageSetAssertionFailedException>();
        }

        [Test]
        public void HasUrlMatching_should_throw_CardImageSetAssertionFailedException_when_trying_to_capture_groups_but_Url_of_all_cards_is_null()
        {
            IList<string> matches;

            var cardImages = Enumerable.Range(1, 5).Select(_ => new CardImage()).ToList();

            var sut = new CardImageSetAssertions(cardImages);

            Action act = () => sut.HasUrlMatching(".*", "(.*)", out matches);

            act.ShouldThrow<CardImageSetAssertionFailedException>();
        }

        [Test]
        public void HasUrlMatching_should_not_output_matches_when_regex_does_not_match_Url_of_any_cards()
        {
            IList<string> matches = null;

            var cardImages = CardImageTestData.CreateRandomCardImages();

            var sut = new CardImageSetAssertions(cardImages);

            Action act = () => sut.HasUrlMatching("non matching regex", "(some text)", out matches);

            act.ShouldThrow<CardImageSetAssertionFailedException>();
            matches.Should().BeNull();
        }

        [Test]
        public void HasUrlMatching_should_not_output_matches_when_groupMatchingRegex_does_not_match_Url_of_any_card()
        {
            IList<string> matches;

            var cardImages = CardImageTestData.CreateRandomCardImages();

            var sut = new CardImageSetAssertions(cardImages);

            sut.HasUrlMatching(".*", "(non matching)", out matches);

            matches.Should().BeNull();
        }

        [Test]
        public void HasUrlMatching_should_output_matches_when_groupMatchingRegex_matches_Url_of_any_card()
        {
            IList<string> matches;

            const string someUrl = "some text";
            var cardImages = CardImageTestData.CreateCardImageSetWithOneMessageThatHasSetProperties(url: someUrl);

            var sut = new CardImageSetAssertions(cardImages);

            sut.HasUrlMatching(someUrl, $"({someUrl})", out matches);

            matches.First().Should().Be(someUrl);
        }

        [Test]
        public void HasUrlMatching_should_output_multiple_matches_when_groupMatchingRegex_matches_Url_several_times_for_a_single_card()
        {
            IList<string> matches;

            const string someUrl = "some text";
            var cardImages = CardImageTestData.CreateCardImageSetWithOneMessageThatHasSetProperties(url: someUrl);

            var sut = new CardImageSetAssertions(cardImages);

            const string match1 = "some";
            const string match2 = "text";
            sut.HasUrlMatching(someUrl, $"({match1}) ({match2})", out matches);

            matches.Should().Contain(match1, match2);
        }

        [Test]
        public void HasUrlMatching_should_output_multiple_matches_when_groupMatchingRegex_matches_Url_on_multiple_cards()
        {
            IList<string> matches;

            var cardImages = CardImageTestData.CreateRandomCardImages();
            cardImages.Add(new CardImage(url: "some text"));
            cardImages.Add(new CardImage(url: "same text"));

            var sut = new CardImageSetAssertions(cardImages);

            sut.HasUrlMatching(".*", @"(s[oa]me) (text)", out matches);

            matches.Should().Contain("some", "same", "text");
        }

        [Test]
        public void HasUrlMatching_should_throw_ArgumentNullException_if_regex_is_null()
        {
            var cardImages = CardImageTestData.CreateRandomCardImages();

            var sut = new CardImageSetAssertions(cardImages);

            Action act = () => sut.HasUrlMatching(null);

            act.ShouldThrow<ArgumentNullException>();
        }

        [Test]
        public void HasUrlMatching_should_throw_ArgumentNullException_when_capturing_groups_if_regex_is_null()
        {
            IList<string> matches;

            var cardImages = CardImageTestData.CreateRandomCardImages();

            var sut = new CardImageSetAssertions(cardImages);

            Action act = () => sut.HasUrlMatching(null, "(.*)", out matches);

            act.ShouldThrow<ArgumentNullException>();
        }

        [Test]
        public void HasUrlMatching_should_throw_ArgumentNullException_if_groupMatchRegex_is_null()
        {
            IList<string> matches;

            var cardImages = CardImageTestData.CreateRandomCardImages();

            var sut = new CardImageSetAssertions(cardImages);

            Action act = () => sut.HasUrlMatching("(.*)", null, out matches);

            act.ShouldThrow<ArgumentNullException>();
        }
    }
}
