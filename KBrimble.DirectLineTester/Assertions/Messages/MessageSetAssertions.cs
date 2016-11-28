using System;
using System.Collections.Generic;
using System.Linq;
using KBrimble.DirectLineTester.Assertions.Attachments;
using KBrimble.DirectLineTester.Exceptions;
using Microsoft.Bot.Connector.DirectLine.Models;

namespace KBrimble.DirectLineTester.Assertions.Messages
{
    public class MessageSetAssertions : IMessageAssertions
    {
        private readonly IEnumerable<Message> _messageSet;

        public MessageSetAssertions(MessageSet messageSet) : this(messageSet.Messages) {}

        public MessageSetAssertions(IEnumerable<Message> messageSet)
        {
            _messageSet = messageSet;
        }

        public IMessageAssertions BeFrom(string messageFrom)
        {
            if (messageFrom == null)
                throw new ArgumentNullException(nameof(messageFrom));

            var passedAssertion = false;
            foreach (var message in _messageSet)
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
            if (regex == null)
                throw new ArgumentNullException(nameof(regex));

            var passedAssertion = false;
            foreach (var message in _messageSet)
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

        public IMessageAssertions HaveTextMatching(string regex, string groupMatchRegex, out IList<string> matchedGroups)
        {
            if (regex == null)
                throw new ArgumentNullException(nameof(regex));
            if (groupMatchRegex == null)
                throw new ArgumentNullException(nameof(groupMatchRegex));

            matchedGroups = null;
            var totalMatches = new List<string>();
            var passedAssertion = false;
            foreach (var message in _messageSet)
            {
                try
                {
                    IList<string> matches;
                    message.Should().HaveTextMatching(regex, groupMatchRegex, out matches);
                    if (matches != null && matches.Any())
                        totalMatches.AddRange(matches);
                }
                catch (MessageAssertionFailedException)
                {
                    continue;
                }
                passedAssertion = true;
            }

            if (!passedAssertion)
                throw new MessageSetAssertionFailedException();

            if (totalMatches.Any())
                matchedGroups = totalMatches;
            return this;
        }

        public IMessageAttachmentAssertions HaveAttachment()
        {
            return new MessageSetAttachmentAssertions(_messageSet);
        }
    }
}
