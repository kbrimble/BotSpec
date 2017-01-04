using Expecto.Assertions.Attachments;
using Expecto.Assertions.Messages;
using FluentAssertions;
using Microsoft.Bot.Connector.DirectLine.Models;
using NUnit.Framework;

namespace Expecto.Tests.Unit.MessageAssertionTests.When_testing_messages
{
    [TestFixture]
    public class For_attachments
    {
        [Test]
        public void Attachment_should_return_MessageAttachmentAssertions()
        {
            var message = new Message();

            var sut = new MessageAssertions(message);

            sut.WithAttachment().Should().BeAssignableTo<MessageAttachmentAssertions>().And.NotBeNull();
        }
    }
}
