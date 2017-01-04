using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Expecto.Assertions.Cards.CardComponents;
using Expecto.Models.Cards;
using Expecto.Tests.Unit.TestData;
using FluentAssertions;
using NUnit.Framework;

// ReSharper disable ObjectCreationAsStatement

namespace Expecto.Tests.Unit.CardAssertionTests.CardComponentAssertionTests.When_testing_card_image_sets
{
    [TestFixture]
    public class When_creating_card_image_set_assertions
    {
        [Test]
        public void Constructor_should_throw_ArgumentNullException_when_CardImage_list_is_null()
        {
            Action act = () => new CardImageSetAssertions((IList<CardImage>)null);
            act.ShouldThrow<ArgumentNullException>();
        }

        [Test]
        public void Constructor_should_throw_ArgumentNullException_when_IHaveImages_list_is_null()
        {
            Action act = () => new CardImageSetAssertions((IEnumerable<IHaveImages>)null);
            act.ShouldThrow<ArgumentNullException>();
        }

        [Test]
        public void Constructor_should_throw_ArgumentNullException_when_IHaveImage_list_is_null()
        {
            Action act = () => new CardImageSetAssertions((IEnumerable<IHaveAnImage>)null);
            act.ShouldThrow<ArgumentNullException>();
        }

        [Test]
        public void Only_non_null_CardImages_should_be_available_for_assertion()
        {
            var nonNullCardImages = CardImageTestData.CreateRandomCardImages();
            var inputList = new List<CardImage> { null };
            inputList.AddRange(nonNullCardImages);

            var sut = new CardImageSetAssertions(inputList);
            sut.CardImages.ShouldBeEquivalentTo(nonNullCardImages);
        }

        [Test]
        [SuppressMessage("ReSharper", "PossibleMultipleEnumeration")]
        public void Only_non_null_IHaveImageses_should_be_available_for_assertion()
        {
            var images = CardImageTestData.CreateRandomCardImages();
            IEnumerable<IHaveImages> nonNullIHaveImageses = ThumbnailCardTestData.CreateThumbnailCardSetWithAllCardsWithSetProperties(images: images);
            var cardActions = new List<IHaveImages> { null };
            cardActions.AddRange(nonNullIHaveImageses);
            var inputList = cardActions.Cast<IHaveImages>();

            var sut = new CardImageSetAssertions(inputList);
            sut.CardImages.ShouldBeEquivalentTo(nonNullIHaveImageses.SelectMany(x => x?.Images));
        }

        [Test]
        [SuppressMessage("ReSharper", "PossibleMultipleEnumeration")]
        public void Only_non_null_IHaveAnImages_should_be_available_for_assertion()
        {
            var image = new CardImage();
            IEnumerable<IHaveAnImage> nonNullIHaveAnImages = ReceiptItemTestData.CreateReceiptItemSetWithAllItemsWithSetProperties(image: image);
            var iHaveAnImages = new List<IHaveAnImage> { new ReceiptItem(image: null) };
            iHaveAnImages.AddRange(nonNullIHaveAnImages);
            var inputList = iHaveAnImages.Cast<IHaveAnImage>();

            var sut = new CardImageSetAssertions(inputList);
            sut.CardImages.ShouldAllBeEquivalentTo(nonNullIHaveAnImages.Select(x => x.Image));
        }
    }
}
