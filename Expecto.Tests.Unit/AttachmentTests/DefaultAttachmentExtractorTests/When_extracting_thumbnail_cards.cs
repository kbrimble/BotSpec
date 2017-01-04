using System.Collections.Generic;
using System.Linq;
using Expecto.Attachments;
using Expecto.Models.Cards;
using FluentAssertions;
using Microsoft.Bot.Connector.DirectLine.Models;
using Newtonsoft.Json;
using NSubstitute;
using NUnit.Framework;

namespace Expecto.Tests.Unit.AttachmentTests.DefaultAttachmentExtractorTests
{
    [TestFixture]
    public class When_extracting_thumbnail_cards
    {
        private IAttachmentRetriever _retriever;

        [SetUp]
        public void SetUp()
        {
            _retriever = Substitute.For<IAttachmentRetriever>();
            AttachmentRetrieverSettings.AttachmentRetrieverType = AttachmentRetrieverType.Custom;
            AttachmentRetrieverSettings.CustomAttachmentRetriever = _retriever;
        }

        [Test]
        public void Only_attachments_with_valid_content_type_should_be_extracted()
        {
            const string validContentType = ThumbnailCard.ContentType;
            var validThumbnailCard = new ThumbnailCard(text: "some text");
            var validJson = JsonConvert.SerializeObject(validThumbnailCard);
            var validAttachment = new Attachment("validUrl1", validContentType);

            const string invalidContentType = "invalidContentType";
            var invalidAttachment = new Attachment("validUrl2", invalidContentType);

            var attachments = new List<Attachment> { validAttachment, invalidAttachment };
            var message = new Message(attachments: attachments);

            _retriever.GetAttachmentsFromUrls(Arg.Is<string[]>(arr => arr.Length == 1)).Returns(new[] {validJson});
            _retriever.GetAttachmentsFromUrls(Arg.Is<string[]>(arr => arr.Length == 2)).Returns(new[] {validJson, validJson});

            var sut = new DefaultAttachmentExtractor();

            var returnedCards = sut.ExtractCards<ThumbnailCard>(message).ToList();

            returnedCards.Count.Should().Be(1);
        }

        [Test]
        public void Only_attachments_with_valid_json_should_be_extracted()
        {
            var validJson = JsonConvert.SerializeObject(new ThumbnailCard(text: "valid"));
            var inValidJson = validJson + "some extra text";
            var attachment = new Attachment(contentType: ThumbnailCard.ContentType);
            var message = new Message(attachments: new[] {attachment, attachment});

            _retriever.GetAttachmentsFromUrls(Arg.Any<string[]>()).Returns(new[] {validJson, inValidJson});

            var sut = new DefaultAttachmentExtractor();

            var returnedCards = sut.ExtractCards<ThumbnailCard>(message).ToList();

            returnedCards.Count.Should().Be(1);
        }

        [Test]
        public void All_attachments_with_correct_content_type_will_be_extracted_regardless_of_type()
        {
            var thumbnailCard = new ThumbnailCard(text: "some text");
            var thumbnailJson = JsonConvert.SerializeObject(thumbnailCard);
            var thumbnailAttachment = new Attachment("validUrl1", ThumbnailCard.ContentType);

            var someOtherType = new {SomeField = "some text"};
            var someOtherTypeJson = JsonConvert.SerializeObject(someOtherType);
            var someOtherTypeAttachment = new Attachment("validUrl2", ThumbnailCard.ContentType);

            var attachments = new List<Attachment> { thumbnailAttachment, someOtherTypeAttachment };
            var message = new Message(attachments: attachments);

            _retriever.GetAttachmentsFromUrls(Arg.Any<string[]>()).Returns(new[] {thumbnailJson, someOtherTypeJson});

            var sut = new DefaultAttachmentExtractor();

            var returnedCards = sut.ExtractCards<ThumbnailCard>(message).ToList();

            returnedCards.Count.Should().Be(2);
            returnedCards.Should().Contain(card => card.Text == null);
        }
    }
}
