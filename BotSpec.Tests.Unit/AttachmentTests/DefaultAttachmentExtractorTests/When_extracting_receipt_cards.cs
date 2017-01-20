using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using BotSpec.Attachments;
using FluentAssertions;
using Microsoft.Bot.Connector.DirectLine;
using Newtonsoft.Json;
using NSubstitute;
using NUnit.Framework;

namespace BotSpec.Tests.Unit.AttachmentTests.DefaultAttachmentExtractorTests
{
    [TestFixture]
    public class When_extracting_receipt_cards
    {
        private IAttachmentRetriever _retriever;
        private static readonly string ValidContentType = DefaultAttachmentExtractor.ContentTypeMap.Map(typeof(ReceiptCard));

        [SetUp]
        public void SetUp()
        {
            _retriever = Substitute.For<IAttachmentRetriever>();
            AttachmentRetrieverSettings.AttachmentRetrieverType = AttachmentRetrieverType.Custom;
            AttachmentRetrieverSettings.CustomAttachmentRetriever = _retriever;
        }

        [Test]
        [SuppressMessage("ReSharper", "ArgumentsStyleLiteral")]
        [SuppressMessage("ReSharper", "ArgumentsStyleNamedExpression")]
        [SuppressMessage("ReSharper", "ReturnValueOfPureMethodIsNotUsed")]
        public void Attachments_with_content_should_be_deserialized_without_being_retrieved()
        {
            var receiptCard = new ReceiptCard("some title");
            var receiptCardJson = JsonConvert.SerializeObject(receiptCard);
            var attachment = new Attachment(ValidContentType, contentUrl: null, content: receiptCardJson);
            var activity = new Activity(attachments: new[] { attachment });

            var sut = new DefaultAttachmentExtractor();
            sut.ExtractCards<ReceiptCard>(activity).ToList();

            _retriever.DidNotReceive().GetAttachmentFromUrl(Arg.Any<string>());
            _retriever.DidNotReceiveWithAnyArgs().GetAttachmentsFromUrls(Arg.Any<string[]>());
        }

        [Test]
        [SuppressMessage("ReSharper", "ArgumentsStyleNamedExpression")]
        [SuppressMessage("ReSharper", "RedundantArgumentDefaultValue")]
        [SuppressMessage("ReSharper", "ArgumentsStyleLiteral")]
        [SuppressMessage("ReSharper", "ReturnValueOfPureMethodIsNotUsed")]
        public void Attachments_with_content_url_should_be_retrieved_by_url()
        {
            const string contentUrl = "URL";
            var receiptCard = new ReceiptCard("some title");
            var receiptCardJson = JsonConvert.SerializeObject(receiptCard);
            _retriever.GetAttachmentsFromUrls(Arg.Any<string[]>()).Returns(new[] { receiptCardJson });
            var attachment = new Attachment(ValidContentType, contentUrl: contentUrl, content: null);
            var activity = new Activity(attachments: new[] { attachment });

            var sut = new DefaultAttachmentExtractor();
            sut.ExtractCards<ReceiptCard>(activity).ToList();

            _retriever.Received().GetAttachmentsFromUrls(Arg.Is<string[]>(x => x.Contains(contentUrl)));
        }

        [Test]
        public void Only_attachments_with_valid_json_should_be_extracted()
        {
            var validJson = JsonConvert.SerializeObject(new ReceiptCard(title: "valid"));
            var invalidJson = validJson + "some extra text";
            var validAttachment = new Attachment(ValidContentType, content: validJson);
            var invalidAttachment = new Attachment(ValidContentType, content: invalidJson);
            var activity = new Activity(attachments: new[] {validAttachment, invalidAttachment });

            var sut = new DefaultAttachmentExtractor();

            var returnedCards = sut.ExtractCards<ReceiptCard>(activity).ToList();

            returnedCards.Count.Should().Be(1);
        }

        [Test]
        public void All_attachments_with_correct_content_type_will_be_extracted_regardless_of_type()
        {
            var receiptCard = new ReceiptCard(title: "some text");
            var receiptJson = JsonConvert.SerializeObject(receiptCard);
            var receiptAttachment = new Attachment(ValidContentType, "validUrl1");

            var someOtherType = new {SomeField = "some text"};
            var someOtherTypeJson = JsonConvert.SerializeObject(someOtherType);
            var someOtherTypeAttachment = new Attachment(ValidContentType, "validUrl2");

            var attachments = new List<Attachment> { receiptAttachment, someOtherTypeAttachment };
            var activity = new Activity(attachments: attachments);

            _retriever.GetAttachmentsFromUrls(Arg.Any<string[]>()).Returns(new[] {receiptJson, someOtherTypeJson});

            var sut = new DefaultAttachmentExtractor();

            var returnedCards = sut.ExtractCards<ReceiptCard>(activity).ToList();

            returnedCards.Count.Should().Be(2);
            returnedCards.Should().Contain(card => card.Title == null);
        }
    }
}
