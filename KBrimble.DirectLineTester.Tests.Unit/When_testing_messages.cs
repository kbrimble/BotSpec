using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using KBrimble.DirectLineTester.Assertions;
using KBrimble.DirectLineTester.Exceptions;
using Microsoft.Bot.Connector.DirectLine.Models;
using NUnit.Framework;

namespace KBrimble.DirectLineTester.Tests.Unit
{
    [TestFixture]
    public class When_testing_messages
    {
        [TestCase("some text")]
        [TestCase("")]
        [TestCase("symbols ([*])?")]
        public void HaveTextMatching_should_pass_if_regex_exactly_matches_message_Text(string messageTextAndRegex)
        {
            var message = new Message(text: messageTextAndRegex);

            var sut = new MessageAssertions(message);

            Action act = () => sut.HaveTextMatching(messageTextAndRegex);

            act.ShouldNotThrow<Exception>();
        }

        [TestCase("some text", "SOME TEXT")]
        [TestCase("SYMBOLS ([*])?", "symbols ([*])?")]
        public void HaveTextMatching_should_pass_regardless_of_case(string messageText, string regex)
        {
            var message = new Message(text: messageText);

            var sut = new MessageAssertions(message);

            Action act = () => sut.HaveTextMatching(regex);

            act.ShouldNotThrow<Exception>();
        }

        [TestCase("some text", "so.*xt")]
        [TestCase("some text", "[a-z ]*")]
        [TestCase("some text", "s(ome tex)t")]
        public void HaveTextMatching_should_pass_when_using_standard_regex_features(string messageText, string regex)
        {
            var message = new Message(text: messageText);

            var sut = new MessageAssertions(message);

            Action act = () => sut.HaveTextMatching(regex);

            act.ShouldNotThrow<Exception>();
        }

        [TestCase("some text", "some text!")]
        [TestCase("some text", "^[j-z ]*$")]
        [TestCase("some text", "s{12}")]
        public void HaveTextMatching_should_throw_MessageAssertionFailedException_for_non_matching_regexes(string messageText, string regex)
        {
            var message = new Message(text: messageText);

            var sut = new MessageAssertions(message);

            Action act = () => sut.HaveTextMatching(regex);

            act.ShouldThrow<MessageAssertionFailedException>();
        }

        [Test]
        public void HaveTextMatching_should_not_output_matches_when_regex_does_not_match_text()
        {
            IEnumerable<string> matches = null;

            var message = new Message(text: "some text");

            var sut = new MessageAssertions(message);

            Action act = () => sut.HaveTextMatching("non matching regex", "(some text)", out matches);

            act.ShouldThrow<MessageAssertionFailedException>();
            matches.Should().BeNull();
        }

        [Test]
        public void HaveTextMatching_should_not_output_matches_when_groupMatchingRegex_does_not_match_text()
        {
            IEnumerable<string> matches = null;

            var message = new Message(text: "some text");

            var sut = new MessageAssertions(message);

            sut.HaveTextMatching("some text", "(non matching)", out matches);

            matches.Should().BeNull();
        }

        [Test]
        public void HaveTextMatching_should_ouput_matches_when_groupMatchingRegex_does_match_text()
        {
            IEnumerable<string> matches = null;

            const string someText = "some text";
            var message = new Message(text: someText);

            var sut = new MessageAssertions(message);

            sut.HaveTextMatching(someText, $"({someText})", out matches);

            matches.First().Should().Be(someText);
        }

        [Test]
        public void HaveTextMatching_should_output_multiple_matches_when_groupMatchingRegex_matches_text_several_times()
        {
            IEnumerable<string> matches = null;

            const string someText = "some text";
            var message = new Message(text: someText);

            var sut = new MessageAssertions(message);

            var match1 = "some";
            var match2 = "text";
            sut.HaveTextMatching(someText, $"({match1}) ({match2})", out matches);

            matches.Should().Contain(match1, match2);
        }

        [Test]
        public void BeFrom_should_throw_MessageAssertionFailedException_if_input_does_not_match_FromProperty_of_message()
        {
            var fromValue = "fromMe";
            var message = new Message(fromProperty: fromValue);

            var sut = new MessageAssertions(message);

            Action act = () => sut.BeFrom("someoneElse");

            act.ShouldThrow<MessageAssertionFailedException>();
        }

        [Test]
        public void BeFrom_should_not_throw_if_input_only_differers_message_FromProperty_in_case()
        {
            var fromValue = "FROMME";
            var message = new Message(fromProperty: fromValue);

            var sut = new MessageAssertions(message);

            Action act = () => sut.BeFrom("fromMe");

            act.ShouldNotThrow<MessageAssertionFailedException>();
        }

        [Test]
        public void BeFrom_should_not_throw_if_input_exactly_matches_message_FromProperty()
        {
            var fromValue = "fromMe";
            var message = new Message(fromProperty: fromValue);

            var sut = new MessageAssertions(message);

            Action act = () => sut.BeFrom(fromValue);

            act.ShouldNotThrow<MessageAssertionFailedException>();
        }
    }
}
