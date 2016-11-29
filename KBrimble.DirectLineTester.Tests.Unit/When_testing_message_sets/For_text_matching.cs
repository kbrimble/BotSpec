using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using KBrimble.DirectLineTester.Assertions.Messages;
using KBrimble.DirectLineTester.Exceptions;
using Microsoft.Bot.Connector.DirectLine.Models;
using NUnit.Framework;

namespace KBrimble.DirectLineTester.Tests.Unit.When_testing_message_sets
{
    [TestFixture]
    public class For_text_matching
    {
        [Test]
        public void HaveTextMatching_should_pass_if_regex_exactly_matches_text_of_at_least_1_message()
        {
            const string messageTextAndRegex = "some text";
            var messages = CreateMessageSetWithOneMessageThatHasSetText(messageTextAndRegex);

            var messageSet = new MessageSet(messages, "0");

            var sut = new MessageSetAssertions(messageSet);

            Action act = () => sut.HaveTextMatching(messageTextAndRegex);

            act.ShouldNotThrow<MessageSetAssertionFailedException>();
        }

        [TestCase("some text", "SOME TEXT")]
        [TestCase("SYMBOLS ([*])?", "symbols ([*])?")]
        public void HaveTextMatching_should_pass_if_regex_exactly_matches_text_of_at_least_1_message_regardless_of_case(string messageText, string regex)
        {
            var messages = CreateMessageSetWithOneMessageThatHasSetText(messageText);

            var messageSet = new MessageSet(messages, "0");

            var sut = new MessageSetAssertions(messageSet);

            Action act = () => sut.HaveTextMatching(regex);

            act.ShouldNotThrow<MessageSetAssertionFailedException>();
        }

        [TestCase("some text", "so.*xt")]
        [TestCase("some text", "[a-z ]*")]
        [TestCase("some text", "s(ome tex)t")]
        public void HaveTextMatching_should_pass_when_using_standard_regex_features(string messageText, string regex)
        {
            var messages = CreateMessageSetWithOneMessageThatHasSetText(messageText);

            var messageSet = new MessageSet(messages, "0");

            var sut = new MessageSetAssertions(messageSet);

            Action act = () => sut.HaveTextMatching(regex);

            act.ShouldNotThrow<MessageSetAssertionFailedException>();
        }

        [TestCase("some text!")]
        [TestCase("^[j-z ]*$")]
        [TestCase("s{12}")]
        public void HaveTextMatching_should_throw_MessageSetAssertionFailedException_when_regex_matches_no_messages(string regex)
        {
            var messages = CreateRandomMessages();

            var messageSet = new MessageSet(messages, "0");

            var sut = new MessageSetAssertions(messageSet);

            Action act = () => sut.HaveTextMatching(regex);

            act.ShouldThrow<MessageSetAssertionFailedException>();
        }

        [Test]
        public void HaveTextMatching_should_throw_MessageSetAssertionFailedException_when_text_of_all_messages_is_null()
        {
            var messages = Enumerable.Range(1, 5).Select(_ => new Message()).ToList();

            var messageSet = new MessageSet(messages, "0");

            var sut = new MessageSetAssertions(messageSet);

            Action act = () => sut.HaveTextMatching(".*");

            act.ShouldThrow<MessageSetAssertionFailedException>();
        }

        [Test]
        public void HaveTextMatching_should_throw_MessageSetAssertionFailedException_when_trying_to_capture_groups_but_text_is_null()
        {
            IList<string> matches;

            var messages = Enumerable.Range(1, 5).Select(_ => new Message()).ToList();

            var messageSet = new MessageSet(messages, "0");

            var sut = new MessageSetAssertions(messageSet);

            Action act = () => sut.HaveTextMatching(".*", "(.*)", out matches);

            act.ShouldThrow<MessageSetAssertionFailedException>();
        }

        [Test]
        public void HaveTextMatching_should_not_output_matches_when_regex_does_not_match_text_of_any_messages()
        {
            IList<string> matches = null;

            var messages = CreateRandomMessages();

            var messageSet = new MessageSet(messages, "0");

            var sut = new MessageSetAssertions(messageSet);

            Action act = () => sut.HaveTextMatching("non matching regex", "(some text)", out matches);

            act.ShouldThrow<MessageSetAssertionFailedException>();
            matches.Should().BeNull();
        }

        [Test]
        public void HaveTextMatching_should_not_output_matches_when_groupMatchingRegex_does_not_match_text_of_any_messages()
        {
            IList<string> matches;

            var messages = CreateRandomMessages();

            var messageSet = new MessageSet(messages, "0");

            var sut = new MessageSetAssertions(messageSet);

            sut.HaveTextMatching(".*", "(non matching)", out matches);

            matches.Should().BeNull();
        }

        [Test]
        public void HaveTextMatching_should_output_matches_when_groupMatchingRegex_matches_text_of_any_message()
        {
            IList<string> matches;

            const string someText = "some text";
            var messages = CreateMessageSetWithOneMessageThatHasSetText(someText);

            var messageSet = new MessageSet(messages, "0");

            var sut = new MessageSetAssertions(messageSet);

            sut.HaveTextMatching(someText, $"({someText})", out matches);

            matches.First().Should().Be(someText);
        }

        [Test]
        public void HaveTextMatching_should_output_multiple_matches_when_groupMatchingRegex_matches_text_several_times_for_a_single_message()
        {
            IList<string> matches;

            const string someText = "some text";
            var messages = CreateMessageSetWithOneMessageThatHasSetText(someText);

            var messageSet = new MessageSet(messages, "0");

            var sut = new MessageSetAssertions(messageSet);

            var match1 = "some";
            var match2 = "text";
            sut.HaveTextMatching(someText, $"({match1}) ({match2})", out matches);

            matches.Should().Contain(match1, match2);
        }

        [Test]
        public void HaveTextMatching_should_output_multiple_matches_when_groupMatchingRegex_matches_text_on_multiple_messages()
        {
            IList<string> matches;

            var messages = CreateRandomMessages();
            messages.Add(new Message(text: "some text"));
            messages.Add(new Message(text: "same text"));

            var messageSet = new MessageSet(messages, "0");

            var sut = new MessageSetAssertions(messageSet);

            sut.HaveTextMatching(".*", @"(s[oa]me) (text)", out matches);

            matches.Should().Contain("some", "same", "text");
        }

        [Test]
        public void HaveTextMatching_should_throw_ArgumentNullException_if_regex_is_null()
        {
            var messages = Enumerable.Range(1, 5).Select(i => new Message(text: $"text{i}")).ToList();

            var messageSet = new MessageSet(messages, "0");

            var sut = new MessageSetAssertions(messageSet);

            Action act = () => sut.HaveTextMatching(null);

            act.ShouldThrow<ArgumentNullException>();
        }

        [Test]
        public void HaveTextMatching_should_throw_ArgumentNullException_if_groupMatchRegex_is_null()
        {
            IList<string> matches;

            var messages = Enumerable.Range(1, 5).Select(i => new Message(text: $"text{i}")).ToList();

            var messageSet = new MessageSet(messages, "0");

            var sut = new MessageSetAssertions(messageSet);

            Action act = () => sut.HaveTextMatching(".*", null, out matches);

            act.ShouldThrow<ArgumentNullException>();
        }

        private static List<Message> CreateMessageSetWithOneMessageThatHasSetText(string messageText)
        {
            var matchingMessage = new Message(text: messageText);
            var messages = CreateRandomMessages();
            messages.Add(matchingMessage);
            return messages;
        }

        private static List<Message> CreateRandomMessages()
        {
            var messages = Enumerable.Range(1, 5).Select(i => new Message(text: $"text {i}")).ToList();
            return messages;
        }

    }
}
