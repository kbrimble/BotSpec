﻿using BotSpec.Assertions.Attachments;
using BotSpec.Assertions.Cards;
using BotSpec.Attachments;
using BotSpec.Tests.Unit.TestData;
using FluentAssertions;
using Microsoft.Bot.Connector.DirectLine;
using NSubstitute;
using NUnit.Framework;

namespace BotSpec.Tests.Unit.ActivityAttachmentAssertionTests
{
    [TestFixture]
    public class When_testing_activity_attachments
    {
        private IAttachmentExtractor _extractor;

        [SetUp]
        public void SetUp()
        {
            AttachmentRetrieverSettings.AttachmentRetrieverType = AttachmentRetrieverType.Default;
            AttachmentExtractorSettings.AttachmentExtractorType = AttachmentExtractorType.Custom;
            _extractor = Substitute.For<IAttachmentExtractor>();
            AttachmentExtractorSettings.CustomAttachmentExtractor = _extractor;
        }

        [Test]
        public void Extracted_thumbnail_cards_are_used_to_create_thumbnail_card_set_assertions()
        {
            var thumbnailCards = ThumbnailCardTestData.CreateRandomThumbnailCards();
            _extractor.ExtractCards<ThumbnailCard>(Arg.Any<Activity>()).Returns(thumbnailCards);

            var sut = new ActivityAttachmentAssertions(new Activity());

            var result = sut.OfTypeThumbnailCard();
            result.Should().BeAssignableTo<ThumbnailCardSetAssertions>();
            var assertions = (ThumbnailCardSetAssertions) result;
            assertions.ThumbnailCards.ShouldBeEquivalentTo(thumbnailCards);
        }

        [Test]
        public void Extracted_hero_cards_are_used_to_create_hero_card_set_assertions()
        {
            var heroCards = HeroCardTestData.CreateRandomHeroCards();
            _extractor.ExtractCards<HeroCard>(Arg.Any<Activity>()).Returns(heroCards);

            var sut = new ActivityAttachmentAssertions(new Activity());

            var result = sut.OfTypeHeroCard();
            result.Should().BeAssignableTo<HeroCardSetAssertions>();
            var assertions = (HeroCardSetAssertions) result;
            assertions.HeroCards.ShouldBeEquivalentTo(heroCards);
        }

        [Test]
        public void Extracted_receipt_cards_are_used_to_create_receipt_card_set_assertions()
        {
            var receiptCards = ReceiptCardTestData.CreateRandomReceiptCards();
            _extractor.ExtractCards<ReceiptCard>(Arg.Any<Activity>()).Returns(receiptCards);

            var sut = new ActivityAttachmentAssertions(new Activity());

            var result = sut.OfTypeReceiptCard();
            result.Should().BeAssignableTo<ReceiptCardSetAssertions>();
            var assertions = (ReceiptCardSetAssertions) result;
            assertions.ReceiptCards.ShouldBeEquivalentTo(receiptCards);
        }

        [Test]
        public void Extracted_signin_cards_are_used_to_create_signin_card_set_assertions()
        {
            var signinCards = SigninCardTestData.CreateRandomSigninCards();
            _extractor.ExtractCards<SigninCard>(Arg.Any<Activity>()).Returns(signinCards);

            var sut = new ActivityAttachmentAssertions(new Activity());

            var result = sut.OfTypeSigninCard();
            result.Should().BeAssignableTo<SigninCardSetAssertions>();
            var assertions = (SigninCardSetAssertions) result;
            assertions.SigninCards.ShouldBeEquivalentTo(signinCards);
        }
    }
}
