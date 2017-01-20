using System;
using System.Collections.Generic;
using BotSpec.Assertions.Cards.CardComponents;
using BotSpec.Tests.Unit.TestData;
using FluentAssertions;
using Microsoft.Bot.Connector.DirectLine;
using NUnit.Framework;

// ReSharper disable ObjectCreationAsStatement

namespace BotSpec.Tests.Unit.CardAssertionTests.CardComponentAssertionTests.When_testing_card_image_sets
{
    [TestFixture]
    public class When_creating_card_image_set_assertions
    {
        [Test]
        public void Constructor_should_throw_ArgumentNullException_when_CardImage_list_is_null()
        {
            Action act = () => new CardImageSetAssertions(null);
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
    }
}
