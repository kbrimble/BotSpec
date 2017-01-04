using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using KBrimble.DirectLineTester.Assertions.Cards.CardComponents;
using KBrimble.DirectLineTester.Exceptions;
using KBrimble.DirectLineTester.Models.Cards;
using NUnit.Framework;

namespace KBrimble.DirectLineTester.Tests.Unit.CardAssertionTests.CardComponentAssertionTests.When_testing_card_images
{
    [TestFixture]
    public class For_a_url_matching
    {
        [TestCase("some text")]
        [TestCase("")]
        [TestCase("symbols ([*])?")]
        public void UrlMatching_should_pass_if_regex_exactly_matches_message_Url(string urlAndRegex)
        {
            var cardImage = new CardImage(url: urlAndRegex);

            var sut = new CardImageAssertions(cardImage);

            Action act = () => sut.UrlMatching(urlAndRegex);

            act.ShouldNotThrow<Exception>();
        }

        [TestCase("some text", "SOME TEXT")]
        [TestCase(@"SYMBOLS ([*])?", @"symbols ([*])?")]
        public void UrlMatching_should_pass_regardless_of_case(string url, string regex)
        {
            var cardImage = new CardImage(url: url);

            var sut = new CardImageAssertions(cardImage);

            Action act = () => sut.UrlMatching(regex);

            act.ShouldNotThrow<Exception>();
        }

        [TestCase("some text", "so.*xt")]
        [TestCase("some text", "[a-z ]*")]
        [TestCase("some text", "s(ome tex)t")]
        public void UrlMatching_should_pass_when_using_standard_regex_features(string url, string regex)
        {
            var cardImage = new CardImage(url: url);

            var sut = new CardImageAssertions(cardImage);

            Action act = () => sut.UrlMatching(regex);

            act.ShouldNotThrow<Exception>();
        }

        [TestCase("some text", "some text!")]
        [TestCase("some text", "^[j-z ]*$")]
        [TestCase("some text", "s{12}")]
        public void UrlMatching_should_throw_CardImageAssertionFailedException_for_non_matching_regexes(string url, string regex)
        {
            var cardImage = new CardImage(url: url);

            var sut = new CardImageAssertions(cardImage);

            Action act = () => sut.UrlMatching(regex);

            act.ShouldThrow<CardImageAssertionFailedException>();
        }

        [Test]
        public void UrlMatching_should_not_output_matches_when_regex_does_not_match_text()
        {
            IList<string> matches = null;

            var cardImage = new CardImage(url: "some text");

            var sut = new CardImageAssertions(cardImage);

            Action act = () => sut.UrlMatching("non matching regex", "(some text)", out matches);

            act.ShouldThrow<CardImageAssertionFailedException>();
            matches.Should().BeNull();
        }

        [Test]
        public void UrlMatching_should_not_output_matches_when_groupMatchingRegex_does_not_match_text()
        {
            IList<string> matches;

            var cardImage = new CardImage(url: "some text");

            var sut = new CardImageAssertions(cardImage);

            sut.UrlMatching("some text", "(non matching)", out matches);

            matches.Should().BeNull();
        }

        [Test]
        public void UrlMatching_should_output_matches_when_groupMatchingRegex_matches_text()
        {
            IList<string> matches;

            const string someText = "some text";
            var cardImage = new CardImage(url: someText);

            var sut = new CardImageAssertions(cardImage);

            sut.UrlMatching(someText, $"({someText})", out matches);

            matches.First().Should().Be(someText);
        }

        [Test]
        public void UrlMatching_should_output_multiple_matches_when_groupMatchingRegex_matches_text_several_times()
        {
            IList<string> matches;

            const string someText = "some text";
            var cardImage = new CardImage(url: someText);

            var sut = new CardImageAssertions(cardImage);

            const string match1 = "some";
            const string match2 = "text";
            sut.UrlMatching(someText, $"({match1}) ({match2})", out matches);

            matches.Should().Contain(match1, match2);
        }

        [Test]
        public void UrlMatching_should_throw_CardImageAssertionFailedException_when_text_is_null()
        {
            var card = new CardImage();

            var sut = new CardImageAssertions(card);

            Action act = () => sut.UrlMatching("anything");

            act.ShouldThrow<CardImageAssertionFailedException>();
        }

        [Test]
        public void UrlMatching_should_throw_CardImageAssertionFailedException_when_trying_to_capture_groups_but_text_is_null()
        {
            IList<string> matches;
            var card = new CardImage();

            var sut = new CardImageAssertions(card);

            Action act = () => sut.UrlMatching("anything", "(.*)", out matches);

            act.ShouldThrow<CardImageAssertionFailedException>();
        }

        [Test]
        public void UrlMatching_should_throw_ArgumentNullException_if_regex_is_null()
        {
            var card = new CardImage();

            var sut = new CardImageAssertions(card);

            Action act = () => sut.UrlMatching(null);

            act.ShouldThrow<ArgumentNullException>();
        }

        [Test]
        public void UrlMatching_should_throw_ArgumentNullException_if_when_capturing_groups_regex_is_null()
        {
            IList<string> matches;

            var card = new CardImage();

            var sut = new CardImageAssertions(card);

            Action act = () => sut.UrlMatching(null, "(.*)", out matches);

            act.ShouldThrow<ArgumentNullException>();
        }

        [Test]
        public void UrlMatching_should_throw_ArgumentNullException_if_groupMatchRegex_is_null()
        {
            IList<string> matches;

            var card = new CardImage();

            var sut = new CardImageAssertions(card);

            Action act = () => sut.UrlMatching("(.*)", null, out matches);

            act.ShouldThrow<ArgumentNullException>();
        }
    }
}
