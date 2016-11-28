using System;
using System.Collections.Generic;
using System.Linq;
using KBrimble.DirectLineTester.Exceptions;
using Microsoft.Bot.Connector.DirectLine.Models;

namespace KBrimble.DirectLineTester.Assertions
{
    public class MessageSetAssertions : IMessageAssertions
    {
        readonly IEnumerable<Message> messageSet;

        public MessageSetAssertions(MessageSet messageSet)
        {
            this.messageSet = messageSet.Messages;
        }

        public MessageSetAssertions(IEnumerable<Message> messageSet)
        {
            this.messageSet = messageSet;
        }

        public IMessageAssertions BeFrom(string messageFrom)
        {
            var passedAssertion = false;
            foreach (var message in messageSet)
            {
                try
                {
                    message.Should().BeFrom(messageFrom);
                }
                catch (MessageAssertionFailedException)
                {
                    continue;
                }
                passedAssertion = true;
                break;
            }

            if (!passedAssertion)
                throw new MessageSetAssertionFailedException();

            return this;
        }

        public IMessageAssertions HaveTextMatching(string regex)
        {
            var passedAssertion = false;
            foreach (var message in messageSet)
            {
                try
                {
                    message.Should().HaveTextMatching(regex);
                }
                catch (MessageAssertionFailedException)
                {
                    continue;
                }
                passedAssertion = true;
                break;
            }

            if (!passedAssertion)
                throw new MessageSetAssertionFailedException();

            return this;
        }

        public IMessageAssertions HaveTextMatching(string regex, string groupMatchRegex, out IEnumerable<string> matchedGroups)
        {
            matchedGroups = null;
            var totalMatches = new List<string>();
            var passedAssertion = false;
            foreach (var message in messageSet)
            {
                try
                {
                    IEnumerable<string> matches;
                    message.Should().HaveTextMatching(regex, groupMatchRegex, out matches);
                    if (matches.Any())
                        totalMatches.AddRange(matches);
                }
                catch (MessageAssertionFailedException)
                {
                    continue;
                }
                passedAssertion = true;
                break;
            }

            if (!passedAssertion)
                throw new MessageSetAssertionFailedException();

            if (totalMatches.Any())
                matchedGroups = totalMatches;
            return this;
        }

        public IMessageAttachmentAssertions WithAttachment()
        {
            throw new NotImplementedException();
        }
    }
}
