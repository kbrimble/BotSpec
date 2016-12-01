using System;
using System.Collections.Generic;
using KBrimble.DirectLineTester.Assertions.Attachments;
using KBrimble.DirectLineTester.Exceptions;
using Microsoft.Bot.Connector.DirectLine.Models;

namespace KBrimble.DirectLineTester.Assertions.Messages
{
    public class MessageSetAssertions : IMessageAssertions, IThrow<MessageSetAssertionFailedException>
    {
        private readonly IEnumerable<Message> _messageSet;
        private readonly SetHelpers<Message, MessageAssertionFailedException, MessageSetAssertionFailedException> _setHelpers;

        public MessageSetAssertions(MessageSet messageSet) : this(messageSet.Messages) {}

        public MessageSetAssertions(IEnumerable<Message> messageSet)
        {
            _setHelpers = new SetHelpers<Message, MessageAssertionFailedException, MessageSetAssertionFailedException>();
            _messageSet = messageSet;
        }

        public IMessageAssertions HaveFromMatching(string regex)
        {
            if (regex == null)
                throw new ArgumentNullException(nameof(regex));

            _setHelpers.TestSetForMatch(_messageSet, msg => msg.Should().HaveFromMatching(regex), CreateEx(nameof(Message.FromProperty), regex));

            return this;
        }

        public IMessageAssertions HaveFromMatching(string regex, string groupMatchRegex, out IList<string> matchedGroups)
        {
            if (regex == null)
                throw new ArgumentNullException(nameof(regex));
            if (groupMatchRegex == null)
                throw new ArgumentNullException(nameof(groupMatchRegex));

            SetHelpers<Message, MessageAssertionFailedException, MessageSetAssertionFailedException>.TestWithGroups act
                = (Message item, out IList<string> matches) => item.Should().HaveFromMatching(regex, groupMatchRegex, out matches);
            matchedGroups = _setHelpers.TestSetForMatchAndReturnGroups(_messageSet, act, CreateEx(nameof(Message.FromProperty), regex));

            return this;

        }

        public IMessageAssertions HaveTextMatching(string regex)
        {
            if (regex == null)
                throw new ArgumentNullException(nameof(regex));

            _setHelpers.TestSetForMatch(_messageSet, msg => msg.Should().HaveTextMatching(regex), CreateEx(nameof(Message.Text), regex));

            return this;
        }

        public IMessageAssertions HaveTextMatching(string regex, string groupMatchRegex, out IList<string> matchedGroups)
        {
            if (regex == null)
                throw new ArgumentNullException(nameof(regex));
            if (groupMatchRegex == null)
                throw new ArgumentNullException(nameof(groupMatchRegex));

            SetHelpers<Message, MessageAssertionFailedException, MessageSetAssertionFailedException>.TestWithGroups act
                = (Message item, out IList<string> matches) => item.Should().HaveTextMatching(regex, groupMatchRegex, out matches);
            matchedGroups = _setHelpers.TestSetForMatchAndReturnGroups(_messageSet, act, CreateEx(nameof(Message.Text), regex));

            return this;
        }

        public IMessageAttachmentAssertions HaveAttachment()
        {
            return new MessageSetAttachmentAssertions(_messageSet);
        }

        public MessageSetAssertionFailedException CreateEx(string testedProperty, string regex)
        {
            var message = $"Expected one message in set to have property {testedProperty} to match {regex} but none did.";
            return new MessageSetAssertionFailedException(message);
        }
    }
}
