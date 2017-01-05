using System;
using BotSpec.Attachments;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;

namespace BotSpec.Tests.Unit.AttachmentTests.AttachmentExtractorFactoryTests
{
    [TestFixture]
    public class When_getting_attachment_extractor
    {
        [OneTimeSetUp]
        public void SetUp()
        {
            AttachmentRetrieverSettings.AttachmentRetrieverType = AttachmentRetrieverType.Default;
        }

        [Test]
        public void DefaultAttachmentExtractor_should_be_returned_when_Type_is_set_to_default()
        {
            AttachmentExtractorSettings.AttachmentExtractorType = AttachmentExtractorType.Default;
            var result = AttachmentExtractorFactory.GetAttachmentExtractor();

            result.Should().BeOfType<DefaultAttachmentExtractor>();
        }

        [Test]
        public void Custom_attachment_extractor_should_be_returned_when_Type_is_set_to_custom()
        {
            AttachmentExtractorSettings.AttachmentExtractorType = AttachmentExtractorType.Custom;
            var attachmentExtractor = Substitute.For<IAttachmentExtractor>();
            AttachmentExtractorSettings.CustomAttachmentExtractor = attachmentExtractor;

            var result = AttachmentExtractorFactory.GetAttachmentExtractor();
            result.Should().Be(attachmentExtractor);
        }

        [Test]
        public void InvalidOperationException_should_be_thrown_when_Type_is_set_to_custom_but_custom_attachment_extractor_has_not_been_set()
        {
            AttachmentExtractorSettings.AttachmentExtractorType = AttachmentExtractorType.Custom;
            AttachmentExtractorSettings.CustomAttachmentExtractor = null;

            Action act = () => AttachmentExtractorFactory.GetAttachmentExtractor();
            act.ShouldThrow<InvalidOperationException>();
        }
    }
}
