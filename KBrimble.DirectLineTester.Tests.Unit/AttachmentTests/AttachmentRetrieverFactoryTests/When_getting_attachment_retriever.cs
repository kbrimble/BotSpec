using System;
using FluentAssertions;
using KBrimble.DirectLineTester.Attachments;
using NSubstitute;
using NUnit.Framework;

namespace KBrimble.DirectLineTester.Tests.Unit.AttachmentTests.AttachmentRetrieverFactoryTests
{
    [TestFixture]
    public class When_getting_attachment_retriever
    {
        [Test]
        public void DefaultAttachmentRetriever_should_be_returned_when_Type_is_set_to_default()
        {
            AttachmentRetrieverSettings.AttachmentRetrieverType = AttachmentRetrieverType.Default;
            var result = AttachmentRetrieverFactory.GetAttachmentRetriever();

            result.Should().BeOfType<DefaultAttachmentRetriever>();
        }

        [Test]
        public void Custom_attachment_retriever_should_be_returned_when_Type_is_set_to_custom()
        {
            AttachmentRetrieverSettings.AttachmentRetrieverType = AttachmentRetrieverType.Custom;
            var attachmentRetriever = Substitute.For<IAttachmentRetriever>();
            AttachmentRetrieverSettings.CustomAttachmentRetriever = attachmentRetriever;

            var result = AttachmentRetrieverFactory.GetAttachmentRetriever();
            result.Should().Be(attachmentRetriever);
        }

        [Test]
        public void InvalidOperationException_should_be_thrown_when_Type_is_set_to_custom_but_custom_attachment_retriever_has_not_been_set()
        {
            AttachmentRetrieverSettings.AttachmentRetrieverType = AttachmentRetrieverType.Custom;
            AttachmentRetrieverSettings.CustomAttachmentRetriever = null;

            Action act = () => AttachmentRetrieverFactory.GetAttachmentRetriever();
            act.ShouldThrow<InvalidOperationException>();
        }
    }
}
