﻿using System;
using System.Collections.Generic;
using System.Linq;
using BotSpec.Assertions.Cards.CardComponents;
using BotSpec.Exceptions;
using FluentAssertions;
using Microsoft.Bot.Connector.DirectLine;
using NUnit.Framework;

namespace BotSpec.Tests.Unit.CardAssertionTests.CardComponentAssertionTests.When_testing_card_images
{
    [TestFixture]
    public class For_an_alt_matching
    {
        [TestCase("some text")]
        [TestCase("")]
        [TestCase("symbols ([*])?")]
        public void AltMatching_should_pass_if_regex_exactly_matches_message_Alt(string altAndRegex)
        {
            var cardImage = new CardImage(alt: altAndRegex);

            var sut = new CardImageAssertions(cardImage);

            Action act = () => sut.AltMatching(altAndRegex);

            act.ShouldNotThrow<Exception>();
        }

        [TestCase("some text", "SOME TEXT")]
        [TestCase(@"SYMBOLS ([*])?", @"symbols ([*])?")]
        public void AltMatching_should_pass_regardless_of_case(string alt, string regex)
        {
            var cardImage = new CardImage(alt: alt);

            var sut = new CardImageAssertions(cardImage);

            Action act = () => sut.AltMatching(regex);

            act.ShouldNotThrow<Exception>();
        }

        [TestCase("some text", "so.*xt")]
        [TestCase("some text", "[a-z ]*")]
        [TestCase("some text", "s(ome tex)t")]
        public void AltMatching_should_pass_when_using_standard_regex_features(string alt, string regex)
        {
            var cardImage = new CardImage(alt: alt);

            var sut = new CardImageAssertions(cardImage);

            Action act = () => sut.AltMatching(regex);

            act.ShouldNotThrow<Exception>();
        }

        [TestCase("some text", "some text!")]
        [TestCase("some text", "^[j-z ]*$")]
        [TestCase("some text", "s{12}")]
        public void AltMatching_should_throw_CardImageAssertionFailedException_for_non_matching_regexes(string alt, string regex)
        {
            var cardImage = new CardImage(alt: alt);

            var sut = new CardImageAssertions(cardImage);

            Action act = () => sut.AltMatching(regex);

            act.ShouldThrow<CardImageAssertionFailedException>();
        }

        [Test]
        public void AltMatching_should_not_output_matches_when_regex_does_not_match_text()
        {
            IList<string> matches = null;

            var cardImage = new CardImage(alt: "some text");

            var sut = new CardImageAssertions(cardImage);

            Action act = () => sut.AltMatching("non matching regex", "(some text)", out matches);

            act.ShouldThrow<CardImageAssertionFailedException>();
            matches.Should().BeNull();
        }

        [Test]
        public void AltMatching_should_not_output_matches_when_groupMatchingRegex_does_not_match_text()
        {
            IList<string> matches;

            var cardImage = new CardImage(alt: "some text");

            var sut = new CardImageAssertions(cardImage);

            sut.AltMatching("some text", "(non matching)", out matches);

            matches.Should().BeNull();
        }

        [Test]
        public void AltMatching_should_output_matches_when_groupMatchingRegex_matches_text()
        {
            IList<string> matches;

            const string someText = "some text";
            var cardImage = new CardImage(alt: someText);

            var sut = new CardImageAssertions(cardImage);

            sut.AltMatching(someText, $"({someText})", out matches);

            matches.First().Should().Be(someText);
        }

        [Test]
        public void AltMatching_should_output_multiple_matches_when_groupMatchingRegex_matches_text_several_times()
        {
            IList<string> matches;

            const string someText = "some text";
            var cardImage = new CardImage(alt: someText);

            var sut = new CardImageAssertions(cardImage);

            const string match1 = "some";
            const string match2 = "text";
            sut.AltMatching(someText, $"({match1}) ({match2})", out matches);

            matches.Should().Contain(match1, match2);
        }

        [Test]
        public void AltMatching_should_throw_CardImageAssertionFailedException_when_text_is_null()
        {
            var card = new CardImage();

            var sut = new CardImageAssertions(card);

            Action act = () => sut.AltMatching("anything");

            act.ShouldThrow<CardImageAssertionFailedException>();
        }

        [Test]
        public void AltMatching_should_throw_CardImageAssertionFailedException_when_trying_to_capture_groups_but_text_is_null()
        {
            IList<string> matches;
            var card = new CardImage();

            var sut = new CardImageAssertions(card);

            Action act = () => sut.AltMatching("anything", "(.*)", out matches);

            act.ShouldThrow<CardImageAssertionFailedException>();
        }

        [Test]
        public void AltMatching_should_throw_ArgumentNullException_if_regex_is_null()
        {
            var card = new CardImage();

            var sut = new CardImageAssertions(card);

            Action act = () => sut.AltMatching(null);

            act.ShouldThrow<ArgumentNullException>();
        }

        [Test]
        public void AltMatching_should_throw_ArgumentNullException_if_when_capturing_groups_regex_is_null()
        {
            IList<string> matches;

            var card = new CardImage();

            var sut = new CardImageAssertions(card);

            Action act = () => sut.AltMatching(null, "(.*)", out matches);

            act.ShouldThrow<ArgumentNullException>();
        }

        [Test]
        public void AltMatching_should_throw_ArgumentNullException_if_groupMatchRegex_is_null()
        {
            IList<string> matches;

            var card = new CardImage();

            var sut = new CardImageAssertions(card);

            Action act = () => sut.AltMatching("(.*)", null, out matches);

            act.ShouldThrow<ArgumentNullException>();
        }
    }
}
