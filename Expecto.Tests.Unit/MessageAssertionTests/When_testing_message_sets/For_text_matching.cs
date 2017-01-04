﻿using System;
using System.Collections.Generic;
using System.Linq;
using Expecto.Assertions.Messages;
using Expecto.Exceptions;
using Expecto.Tests.Unit.TestData;
using FluentAssertions;
using Microsoft.Bot.Connector.DirectLine.Models;
using NUnit.Framework;

namespace Expecto.Tests.Unit.MessageAssertionTests.When_testing_message_sets
{
    [TestFixture]
    public class For_text_matching
    {
        [TestCase("some text")]
        [TestCase("")]
        [TestCase("symbols ([*])?")]
        public void TextMatching_should_pass_if_regex_exactly_matches_message_Text_of_one_message(string textAndRegex)
        {
            var messages = MessageTestData.CreateMessageSetWithOneMessageThatHasSetProperties(text: textAndRegex);

            var sut = new MessageSetAssertions(messages);

            Action act = () => sut.TextMatching(textAndRegex);

            act.ShouldNotThrow<Exception>();
        }

        [TestCase("some text", "SOME TEXT")]
        [TestCase(@"SYMBOLS ([*])?", @"symbols ([*])?")]
        public void TextMatching_should_pass_if_regex_exactly_matches_Text_of_at_least_1_message_regardless_of_case(string text, string regex)
        {
            var messages = MessageTestData.CreateMessageSetWithOneMessageThatHasSetProperties(text: text);

            var sut = new MessageSetAssertions(messages);

            Action act = () => sut.TextMatching(regex);

            act.ShouldNotThrow<Exception>();
        }

        [TestCase("some text", "so.*xt")]
        [TestCase("some text", "[a-z ]*")]
        [TestCase("some text", "s(ome tex)t")]
        public void TextMatching_should_pass_when_using_standard_regex_features(string text, string regex)
        {
            var messages = MessageTestData.CreateMessageSetWithOneMessageThatHasSetProperties(text: text);

            var sut = new MessageSetAssertions(messages);

            Action act = () => sut.TextMatching(regex);

            act.ShouldNotThrow<Exception>();
        }

        [TestCase("some text!")]
        [TestCase("^[j-z ]*$")]
        [TestCase("s{12}")]
        public void TextMatching_should_throw_MessageAssertionFailedException_when_regex_matches_no_messages(string regex)
        {
            var messages = MessageTestData.CreateRandomMessages();

            var sut = new MessageSetAssertions(messages);

            Action act = () => sut.TextMatching(regex);

            act.ShouldThrow<MessageAssertionFailedException>();
        }

        [Test]
        public void TextMatching_should_throw_MessageAssertionFailedException_when_Text_of_all_messages_is_null()
        {
            var messages = Enumerable.Range(1, 5).Select(_ => new Message()).ToList();

            var sut = new MessageSetAssertions(messages);

            Action act = () => sut.TextMatching(".*");

            act.ShouldThrow<MessageAssertionFailedException>();
        }

        [Test]
        public void TextMatching_should_throw_MessageAssertionFailedException_when_trying_to_capture_groups_but_Text_of_all_messages_is_null()
        {
            IList<string> matches;

            var messages = Enumerable.Range(1, 5).Select(_ => new Message()).ToList();

            var sut = new MessageSetAssertions(messages);

            Action act = () => sut.TextMatching(".*", "(.*)", out matches);

            act.ShouldThrow<MessageAssertionFailedException>();
        }

        [Test]
        public void TextMatching_should_not_output_matches_when_regex_does_not_match_Text_of_any_messages()
        {
            IList<string> matches = null;

            var messages = MessageTestData.CreateRandomMessages();

            var sut = new MessageSetAssertions(messages);

            Action act = () => sut.TextMatching("non matching regex", "(some text)", out matches);

            act.ShouldThrow<MessageAssertionFailedException>();
            matches.Should().BeNull();
        }

        [Test]
        public void TextMatching_should_not_output_matches_when_groupMatchingRegex_does_not_match_Text_of_any_message()
        {
            IList<string> matches;

            var messages = MessageTestData.CreateRandomMessages();

            var sut = new MessageSetAssertions(messages);

            sut.TextMatching(".*", "(non matching)", out matches);

            matches.Should().BeNull();
        }

        [Test]
        public void TextMatching_should_output_matches_when_groupMatchingRegex_matches_Text_of_any_message()
        {
            IList<string> matches;

            const string someText = "some text";
            var messages = MessageTestData.CreateMessageSetWithOneMessageThatHasSetProperties(text: someText);

            var sut = new MessageSetAssertions(messages);

            sut.TextMatching(someText, $"({someText})", out matches);

            matches.First().Should().Be(someText);
        }

        [Test]
        public void TextMatching_should_output_multiple_matches_when_groupMatchingRegex_matches_Text_several_times_for_a_single_message()
        {
            IList<string> matches;

            const string someText = "some text";
            var messages = MessageTestData.CreateMessageSetWithOneMessageThatHasSetProperties(text: someText);

            var sut = new MessageSetAssertions(messages);

            const string match1 = "some";
            const string match2 = "text";
            sut.TextMatching(someText, $"({match1}) ({match2})", out matches);

            matches.Should().Contain(match1, match2);
        }

        [Test]
        public void TextMatching_should_output_multiple_matches_when_groupMatchingRegex_matches_Text_on_multiple_messages()
        {
            IList<string> matches;

            var messages = MessageTestData.CreateRandomMessages();
            messages.Add(new Message(text: "some text"));
            messages.Add(new Message(text: "same text"));

            var sut = new MessageSetAssertions(messages);

            sut.TextMatching(".*", @"(s[oa]me) (text)", out matches);

            matches.Should().Contain("some", "same", "text");
        }

        [Test]
        public void TextMatching_should_throw_ArgumentNullException_if_regex_is_null()
        {
            var messages = MessageTestData.CreateRandomMessages();

            var sut = new MessageSetAssertions(messages);

            Action act = () => sut.TextMatching(null);

            act.ShouldThrow<ArgumentNullException>();
        }

        [Test]
        public void TextMatching_should_throw_ArgumentNullException_when_capturing_groups_if_regex_is_null()
        {
            IList<string> matches;

            var messages = MessageTestData.CreateRandomMessages();

            var sut = new MessageSetAssertions(messages);

            Action act = () => sut.TextMatching(null, "(.*)", out matches);

            act.ShouldThrow<ArgumentNullException>();
        }

        [Test]
        public void TextMatching_should_throw_ArgumentNullException_if_groupMatchRegex_is_null()
        {
            IList<string> matches;

            var messages = MessageTestData.CreateRandomMessages();

            var sut = new MessageSetAssertions(messages);

            Action act = () => sut.TextMatching("(.*)", null, out matches);

            act.ShouldThrow<ArgumentNullException>();
        }
    }
}