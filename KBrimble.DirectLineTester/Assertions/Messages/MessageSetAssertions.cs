using System;
using System.Collections.Generic;
using KBrimble.DirectLineTester.Assertions.Attachments;
using KBrimble.DirectLineTester.Exceptions;
using Microsoft.Bot.Connector.DirectLine.Models;

namespace KBrimble.DirectLineTester.Assertions.Messages
{
    public class MessageSetAssertions : IMessageAssertions, IThrow<MessageAssertionFailedException>
    {
        private readonly IEnumerable<Message> _messageSet;
        private readonly SetHelpers<Message, MessageAssertionFailedException> _setHelpers;

        public MessageSetAssertions(MessageSet messageSet) : this(messageSet.Messages) {}

        public MessageSetAssertions(IEnumerable<Message> messageSet)
        {
            _messageSet = messageSet;
            _setHelpers = new SetHelpers<Message, MessageAssertionFailedException>();
        }

        public IMessageAssertions FromMatching(string regex)
        {
            if (regex == null)
                throw new ArgumentNullException(nameof(regex));

            _setHelpers.TestSetForMatch(_messageSet, msg => msg.Should().FromMatching(regex), CreateEx(nameof(Message.FromProperty), regex));

            return this;
        }

        public IMessageAssertions FromMatching(string regex, string groupMatchRegex, out IList<string> matchedGroups)
        {
            if (regex == null)
                throw new ArgumentNullException(nameof(regex));
            if (groupMatchRegex == null)
                throw new ArgumentNullException(nameof(groupMatchRegex));

            SetHelpers<Message, MessageAssertionFailedException>.TestWithGroups act
                = (Message item, out IList<string> matches) => item.Should().FromMatching(regex, groupMatchRegex, out matches);
            matchedGroups = _setHelpers.TestSetForMatchAndReturnGroups(_messageSet, act, CreateEx(nameof(Message.FromProperty), regex));

            return this;

        }

        public IMessageAssertions TextMatching(string regex)
        {
            if (regex == null)
                throw new ArgumentNullException(nameof(regex));

            _setHelpers.TestSetForMatch(_messageSet, msg => msg.Should().TextMatching(regex), CreateEx(nameof(Message.Text), regex));

            return this;
        }

        public IMessageAssertions TextMatching(string regex, string groupMatchRegex, out IList<string> matchedGroups)
        {
            if (regex == null)
                throw new ArgumentNullException(nameof(regex));
            if (groupMatchRegex == null)
                throw new ArgumentNullException(nameof(groupMatchRegex));

            SetHelpers<Message, MessageAssertionFailedException>.TestWithGroups act
                = (Message item, out IList<string> matches) => item.Should().TextMatching(regex, groupMatchRegex, out matches);
            matchedGroups = _setHelpers.TestSetForMatchAndReturnGroups(_messageSet, act, CreateEx(nameof(Message.Text), regex));

            return this;
        }

        public IMessageAssertions IdMatching(string regex)
        {
            if (regex == null)
                throw new ArgumentNullException(nameof(regex));

            _setHelpers.TestSetForMatch(_messageSet, msg => msg.Should().IdMatching(regex), CreateEx(nameof(Message.Id), regex));

            return this;
        }

        public IMessageAssertions IdMatching(string regex, string groupMatchRegex, out IList<string> matchedGroups)
        {
            if (regex == null)
                throw new ArgumentNullException(nameof(regex));
            if (groupMatchRegex == null)
                throw new ArgumentNullException(nameof(groupMatchRegex));

            SetHelpers<Message, MessageAssertionFailedException>.TestWithGroups act
                = (Message item, out IList<string> matches) => item.Should().IdMatching(regex, groupMatchRegex, out matches);
            matchedGroups = _setHelpers.TestSetForMatchAndReturnGroups(_messageSet, act, CreateEx(nameof(Message.Id), regex));

            return this;
        }

        public IMessageAttachmentAssertions WithAttachment()
        {
            return new MessageSetAttachmentAssertions(_messageSet);
        }

        public Func<MessageAssertionFailedException> CreateEx(string testedProperty, string regex)
        {
            var message = $"Expected one message in set to have property {testedProperty} to match {regex} but none did.";
            return () => new MessageAssertionFailedException(message);
        }
    }
}
