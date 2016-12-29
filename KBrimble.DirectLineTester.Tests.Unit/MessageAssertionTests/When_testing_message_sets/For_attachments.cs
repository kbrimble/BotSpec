using FluentAssertions;
using KBrimble.DirectLineTester.Assertions.Attachments;
using KBrimble.DirectLineTester.Assertions.Messages;
using KBrimble.DirectLineTester.Tests.Unit.TestData;
using NUnit.Framework;

namespace KBrimble.DirectLineTester.Tests.Unit.MessageAssertionTests.When_testing_message_sets
{
    [TestFixture]
    public class For_attachments
    {
        [Test]
        public void HaveAttachment_should_return_MessageSetAttachmentAssertions()
        {
            var messages = MessageTestData.CreateRandomMessages();

            var sut = new MessageSetAssertions(messages);

            sut.HaveAttachment().Should().BeAssignableTo<MessageSetAttachmentAssertions>().And.NotBeNull();
        }
    }
}
