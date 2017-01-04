using System;
using System.Collections.Generic;
using System.Linq;
using Expecto.Assertions.Messages;
using Expecto.Exceptions;
using FluentAssertions;
using Microsoft.Bot.Connector.DirectLine.Models;
using NUnit.Framework;

namespace Expecto.Tests.Unit.MessageAssertionTests.When_testing_messages
{
    [TestFixture]
    public class For_text_matching
    {
        [Test]
        public void TextMatching_should_throw_ArgumentNullException_when_regex_is_null()
        {
            var message = new Message(text: "valid text");

            var sut = new MessageAssertions(message);

            Action act = () => sut.TextMatching(null);

            act.ShouldThrow<ArgumentNullException>();
        }

        [Test]
        public void TextMatching_should_throw_ArgumentNullException_when_groupMatchRegex_is_null()
        {
            IList<string> matches;

            var message = new Message(text: "valid text");

            var sut = new MessageAssertions(message);

            Action act = () => sut.TextMatching(".*", null, out matches);

            act.ShouldThrow<ArgumentNullException>();
        }

        [TestCase("some text")]
        [TestCase("")]
        [TestCase("symbols ([*])?")]
        public void TextMatching_should_pass_if_regex_exactly_matches_message_Text(string messageTextAndRegex)
        {
            var message = new Message(text: messageTextAndRegex);

            var sut = new MessageAssertions(message);

            Action act = () => sut.TextMatching(messageTextAndRegex);

            act.ShouldNotThrow<Exception>();
        }

        [TestCase("some text", "SOME TEXT")]
        [TestCase("SYMBOLS ([*])?", "symbols ([*])?")]
        public void TextMatching_should_pass_regardless_of_case(string messageText, string regex)
        {
            var message = new Message(text: messageText);

            var sut = new MessageAssertions(message);

            Action act = () => sut.TextMatching(regex);

            act.ShouldNotThrow<Exception>();
        }

        [TestCase("some text", "so.*xt")]
        [TestCase("some text", "[a-z ]*")]
        [TestCase("some text", "s(ome tex)t")]
        public void TextMatching_should_pass_when_using_standard_regex_features(string messageText, string regex)
        {
            var message = new Message(text: messageText);

            var sut = new MessageAssertions(message);

            Action act = () => sut.TextMatching(regex);

            act.ShouldNotThrow<Exception>();
        }

        [TestCase("some text", "some text!")]
        [TestCase("some text", "^[j-z ]*$")]
        [TestCase("some text", "s{12}")]
        public void TextMatching_should_throw_MessageAssertionFailedException_for_non_matching_regexes(string messageText, string regex)
        {
            var message = new Message(text: messageText);

            var sut = new MessageAssertions(message);

            Action act = () => sut.TextMatching(regex);

            act.ShouldThrow<MessageAssertionFailedException>();
        }

        [Test]
        public void TextMatching_should_not_output_matches_when_regex_does_not_match_text()
        {
            IList<string> matches = null;

            var message = new Message(text: "some text");

            var sut = new MessageAssertions(message);

            Action act = () => sut.TextMatching("non matching regex", "(some text)", out matches);

            act.ShouldThrow<MessageAssertionFailedException>();
            matches.Should().BeNull();
        }

        [Test]
        public void TextMatching_should_not_output_matches_when_groupMatchingRegex_does_not_match_text()
        {
            IList<string> matches;

            var message = new Message(text: "some text");

            var sut = new MessageAssertions(message);

            sut.TextMatching("some text", "(non matching)", out matches);

            matches.Should().BeNull();
        }

        [Test]
        public void TextMatching_should_output_matches_when_groupMatchingRegex_matches_text()
        {
            IList<string> matches;

            const string someText = "some text";
            var message = new Message(text: someText);

            var sut = new MessageAssertions(message);

            sut.TextMatching(someText, $"({someText})", out matches);

            matches.First().Should().Be(someText);
        }

        [Test]
        public void TextMatching_should_output_multiple_matches_when_groupMatchingRegex_matches_text_several_times()
        {
            IList<string> matches;

            const string someText = "some text";
            var message = new Message(text: someText);

            var sut = new MessageAssertions(message);

            var match1 = "some";
            var match2 = "text";
            sut.TextMatching(someText, $"({match1}) ({match2})", out matches);

            matches.Should().Contain(match1, match2);
        }


        [Test]
        public void TextMatching_should_throw_MessageAssertionFailedException_when_text_is_null()
        {
            var message = new Message();

            var sut = new MessageAssertions(message);

            Action act = () => sut.TextMatching("anything");

            act.ShouldThrow<MessageAssertionFailedException>();
        }

        [Test]
        public void TextMatching_should_throw_MessageAssertionFailedException_when_trying_to_capture_groups_but_text_is_null()
        {
            IList<string> matches;
            var message = new Message();

            var sut = new MessageAssertions(message);

            Action act = () => sut.TextMatching("anything", "(.*)", out matches);

            act.ShouldThrow<MessageAssertionFailedException>();
        }

        [Test]
        public void TextMatching_should_throw_ArgumentNullException_when_trying_to_capture_groups_but_regex_is_null()
        {
            IList<string> matches;
            var message = new Message(text: "some text");

            var sut = new MessageAssertions(message);

            Action act = () => sut.TextMatching(null, "(.*)", out matches);

            act.ShouldThrow<ArgumentNullException>();
        }
    }
}
