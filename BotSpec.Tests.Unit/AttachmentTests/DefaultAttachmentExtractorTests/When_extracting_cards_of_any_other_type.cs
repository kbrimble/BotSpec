using System;
using System.Linq;
using BotSpec.Attachments;
using FluentAssertions;
using Microsoft.Bot.Connector.DirectLine.Models;
using Newtonsoft.Json;
using NSubstitute;
using NUnit.Framework;

namespace BotSpec.Tests.Unit.AttachmentTests.DefaultAttachmentExtractorTests
{
    [TestFixture]
    public class When_extracting_cards_of_any_other_type
    {
        [OneTimeSetUp]
        public void SetUp()
        {
            AttachmentRetrieverSettings.AttachmentRetrieverType = AttachmentRetrieverType.Default;
        }

        [Test]
        public void Types_that_have_no_string_property_named_ContentType_will_throw_InvalidOperationException()
        {
            var sut = new DefaultAttachmentExtractor();

            // ReSharper disable once ReturnValueOfPureMethodIsNotUsed
            Action act = () => sut.ExtractCards<TypeWithoutContentTypeProperty>(new Message()).ToList();
            act.ShouldThrow<InvalidOperationException>().WithMessage("Cannot get ContentType property of type TypeWithoutContentTypeProperty");
        }

        [Test]
        public void Types_with_string_property_ContentType_can_be_extracted_from_a_message()
        {
            const string contentType = "contentType";
            const string url = "url";
            var attachment = new Attachment(url, contentType);
            var message = new Message(attachments: new[] { attachment });
            TypeWithContentTypeProperty.ContentType = contentType;
            var returnedType = new TypeWithContentTypeProperty();
            var json = JsonConvert.SerializeObject(returnedType);

            var retriever = Substitute.For<IAttachmentRetriever>();
            retriever.GetAttachmentsFromUrls(Arg.Any<string[]>()).Returns(new[] { json });
            AttachmentRetrieverSettings.AttachmentRetrieverType = AttachmentRetrieverType.Custom;
            AttachmentRetrieverSettings.CustomAttachmentRetriever = retriever;

            var sut = new DefaultAttachmentExtractor();
            var result = sut.ExtractCards<TypeWithContentTypeProperty>(message);
            result.Should().HaveCount(1);
        }
    }

    public class TypeWithoutContentTypeProperty {}
    public class TypeWithContentTypeProperty { public static string ContentType; }
}
