using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using KBrimble.DirectLineTester.Assertions.Messages;
using KBrimble.DirectLineTester.Exceptions;
using Microsoft.Bot.Connector.DirectLine.Models;
using NUnit.Framework;

namespace KBrimble.DirectLineTester.Tests.Unit.MessageAssertionTests.When_testing_messages
{
    [TestFixture]
    public class For_from_matching
    {
        [Test]
        public void HaveFromMatching_should_throw_ArgumentNullException_when_regex_is_null()
        {
            var message = new Message(fromProperty: "valid text");

            var sut = new MessageAssertions(message);

            Action act = () => sut.HaveFromMatching(null);

            act.ShouldThrow<ArgumentNullException>();
        }

        [Test]
        public void HaveFromMatching_should_throw_ArgumentNullException_when_groupMatchRegex_is_null()
        {
            IList<string> matches;

            var message = new Message(fromProperty: "valid text");

            var sut = new MessageAssertions(message);

            Action act = () => sut.HaveFromMatching(".*", null, out matches);

            act.ShouldThrow<ArgumentNullException>();
        }

        [TestCase("some text")]
        [TestCase("")]
        [TestCase("symbols ([*])?")]
        public void HaveFromMatching_should_pass_if_regex_exactly_matches_message_From(string messageFromAndRegex)
        {
            var message = new Message(fromProperty: messageFromAndRegex);

            var sut = new MessageAssertions(message);

            Action act = () => sut.HaveFromMatching(messageFromAndRegex);

            act.ShouldNotThrow<Exception>();
        }

        [TestCase("some text", "SOME TEXT")]
        [TestCase("SYMBOLS ([*])?", "symbols ([*])?")]
        public void HaveFromMatching_should_pass_regardless_of_case(string messageFrom, string regex)
        {
            var message = new Message(fromProperty: messageFrom);

            var sut = new MessageAssertions(message);

            Action act = () => sut.HaveFromMatching(regex);

            act.ShouldNotThrow<Exception>();
        }

        [TestCase("some text", "so.*xt")]
        [TestCase("some text", "[a-z ]*")]
        [TestCase("some text", "s(ome tex)t")]
        public void HaveFromMatching_should_pass_when_using_standard_regex_features(string messageFrom, string regex)
        {
            var message = new Message(fromProperty: messageFrom);

            var sut = new MessageAssertions(message);

            Action act = () => sut.HaveFromMatching(regex);

            act.ShouldNotThrow<Exception>();
        }

        [TestCase("some text", "some text!")]
        [TestCase("some text", "^[j-z ]*$")]
        [TestCase("some text", "s{12}")]
        public void HaveFromMatching_should_throw_MessageAssertionFailedException_for_non_matching_regexes(string messageFrom, string regex)
        {
            var message = new Message(fromProperty: messageFrom);

            var sut = new MessageAssertions(message);

            Action act = () => sut.HaveFromMatching(regex);

            act.ShouldThrow<MessageAssertionFailedException>();
        }

        [Test]
        public void HaveFromMatching_should_not_output_matches_when_regex_does_not_match_text()
        {
            IList<string> matches = null;

            var message = new Message(fromProperty: "some text");

            var sut = new MessageAssertions(message);

            Action act = () => sut.HaveFromMatching("non matching regex", "(some text)", out matches);

            act.ShouldThrow<MessageAssertionFailedException>();
            matches.Should().BeNull();
        }

        [Test]
        public void HaveFromMatching_should_not_output_matches_when_groupMatchingRegex_does_not_match_text()
        {
            IList<string> matches;

            var message = new Message(fromProperty: "some text");

            var sut = new MessageAssertions(message);

            sut.HaveFromMatching("some text", "(non matching)", out matches);

            matches.Should().BeNull();
        }

        [Test]
        public void HaveFromMatching_should_output_matches_when_groupMatchingRegex_matches_text()
        {
            IList<string> matches;

            const string someFrom = "some text";
            var message = new Message(fromProperty: someFrom);

            var sut = new MessageAssertions(message);

            sut.HaveFromMatching(someFrom, $"({someFrom})", out matches);

            matches.First().Should().Be(someFrom);
        }

        [Test]
        public void HaveFromMatching_should_output_multiple_matches_when_groupMatchingRegex_matches_text_several_times()
        {
            IList<string> matches;

            const string someFrom = "some text";
            var message = new Message(fromProperty: someFrom);

            var sut = new MessageAssertions(message);

            var match1 = "some";
            var match2 = "text";
            sut.HaveFromMatching(someFrom, $"({match1}) ({match2})", out matches);

            matches.Should().Contain(match1, match2);
        }


        [Test]
        public void HaveFromMatching_should_throw_MessageAssertionFailedException_when_text_is_null()
        {
            var message = new Message();

            var sut = new MessageAssertions(message);

            Action act = () => sut.HaveFromMatching("anything");

            act.ShouldThrow<MessageAssertionFailedException>();
        }

        [Test]
        public void HaveFromMatching_should_throw_MessageAssertionFailedException_when_trying_to_capture_groups_but_text_is_null()
        {
            IList<string> matches;
            var message = new Message();

            var sut = new MessageAssertions(message);

            Action act = () => sut.HaveFromMatching("anything", "(.*)", out matches);

            act.ShouldThrow<MessageAssertionFailedException>();
        }

    }
}
