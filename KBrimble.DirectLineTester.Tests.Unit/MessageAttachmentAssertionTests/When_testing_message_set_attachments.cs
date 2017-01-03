using System.Collections.Generic;
using FluentAssertions;
using KBrimble.DirectLineTester.Assertions.Attachments;
using KBrimble.DirectLineTester.Assertions.Cards;
using KBrimble.DirectLineTester.Attachments;
using KBrimble.DirectLineTester.Models.Cards;
using KBrimble.DirectLineTester.Tests.Unit.TestData;
using Microsoft.Bot.Connector.DirectLine.Models;
using NSubstitute;
using NUnit.Framework;

namespace KBrimble.DirectLineTester.Tests.Unit.MessageAttachmentAssertionTests
{
    [TestFixture]
    public class When_testing_message_set_attachments
    {
        private IAttachmentExtractor _extractor;

        [SetUp]
        public void SetUp()
        {
            AttachmentExtractorSettings.AttachmentExtractorType = AttachmentExtractorType.Custom;
            _extractor = Substitute.For<IAttachmentExtractor>();
            AttachmentExtractorSettings.CustomAttachmentExtractor = _extractor;
        }

        [Test]
        public void Extracted_thumbnail_cards_are_used_to_create_thumbnail_card_set_assertions()
        {
            var thumbnailCards = ThumbnailCardTestData.CreateRandomThumbnailCards();
            _extractor.ExtractCards<ThumbnailCard>(Arg.Any<IEnumerable<Message>>()).Returns(thumbnailCards);

            var sut = new MessageSetAttachmentAssertions(MessageTestData.CreateRandomMessages());

            var result = sut.OfTypeThumbnailCardThat();
            result.Should().BeAssignableTo<ThumbnailCardSetAssertions>();
            var assertions = (ThumbnailCardSetAssertions)result;
            assertions.ThumbnailCards.ShouldBeEquivalentTo(thumbnailCards);
        }

        [Test]
        public void Extracted_hero_cards_are_used_to_create_hero_card_set_assertions()
        {
            var heroCards = HeroCardTestData.CreateRandomHeroCards();
            _extractor.ExtractCards<HeroCard>(Arg.Any<IEnumerable<Message>>()).Returns(heroCards);

            var sut = new MessageSetAttachmentAssertions(MessageTestData.CreateRandomMessages());

            var result = sut.OfTypeHeroCardThat();
            result.Should().BeAssignableTo<HeroCardSetAssertions>();
            var assertions = (HeroCardSetAssertions)result;
            assertions.HeroCards.ShouldBeEquivalentTo(heroCards);
        }

        [Test]
        public void Extracted_receipt_cards_are_used_to_create_receipt_card_set_assertions()
        {
            var receiptCards = ReceiptCardTestData.CreateRandomReceiptCards();
            _extractor.ExtractCards<ReceiptCard>(Arg.Any<IEnumerable<Message>>()).Returns(receiptCards);

            var sut = new MessageSetAttachmentAssertions(MessageTestData.CreateRandomMessages());

            var result = sut.OfTypeReceiptCardThat();
            result.Should().BeAssignableTo<ReceiptCardSetAssertions>();
            var assertions = (ReceiptCardSetAssertions)result;
            assertions.ReceiptCards.ShouldBeEquivalentTo(receiptCards);
        }

        [Test]
        public void Extracted_signin_cards_are_used_to_create_signin_card_set_assertions()
        {
            var signinCards = SigninCardTestData.CreateRandomSigninCards();
            _extractor.ExtractCards<SigninCard>(Arg.Any<IEnumerable<Message>>()).Returns(signinCards);

            var sut = new MessageSetAttachmentAssertions(MessageTestData.CreateRandomMessages());

            var result = sut.OfTypeSigninCardThat();
            result.Should().BeAssignableTo<SigninCardSetAssertions>();
            var assertions = (SigninCardSetAssertions)result;
            assertions.SigninCards.ShouldBeEquivalentTo(signinCards);
        }
    }
}
