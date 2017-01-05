using BotSpec.Assertions.Attachments;
using BotSpec.Assertions.Messages;
using BotSpec.Tests.Unit.TestData;
using FluentAssertions;
using NUnit.Framework;

namespace BotSpec.Tests.Unit.MessageAssertionTests.When_testing_message_sets
{
    [TestFixture]
    public class For_attachments
    {
        [Test]
        public void Attachment_should_return_MessageSetAttachmentAssertions()
        {
            var messages = MessageTestData.CreateRandomMessages();

            var sut = new MessageSetAssertions(messages);

            sut.WithAttachment().Should().BeAssignableTo<MessageSetAttachmentAssertions>().And.NotBeNull();
        }
    }
}
