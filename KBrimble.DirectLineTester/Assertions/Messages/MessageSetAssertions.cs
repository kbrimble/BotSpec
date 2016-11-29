using System;
using System.Collections.Generic;
using KBrimble.DirectLineTester.Assertions.Attachments;
using KBrimble.DirectLineTester.Exceptions;
using Microsoft.Bot.Connector.DirectLine.Models;
using static KBrimble.DirectLineTester.Assertions.SetHelpers;

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

            TestSetForMatch(_messageSet, msg => msg.Should().HaveTextMatching(regex), typeof(MessageAssertionFailedException), new MessageSetAssertionFailedException());

            return this;
        }

        public IMessageAssertions HaveTextMatching(string regex, string groupMatchRegex, out IList<string> matchedGroups)
        {
            if (regex == null)
                throw new ArgumentNullException(nameof(regex));
            if (groupMatchRegex == null)
                throw new ArgumentNullException(nameof(groupMatchRegex));

            TestWithGroups<Message> act = (Message msg, out IList<string> matches) => msg.Should().HaveTextMatching(regex, groupMatchRegex, out matches);
            matchedGroups = TestSetForMatchAndReturnGroups(_messageSet, act, typeof(MessageAssertionFailedException), new MessageSetAssertionFailedException());

            return this;
        }

        public IMessageAttachmentAssertions HaveAttachment()
        {
            return new MessageSetAttachmentAssertions(_messageSet);
        }
    }
}
