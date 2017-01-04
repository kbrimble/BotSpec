using System;
using Expecto.Assertions.Cards.CardComponents;
using Expecto.Models.Cards;
using FluentAssertions;
using NUnit.Framework;

// ReSharper disable ObjectCreationAsStatement

namespace Expecto.Tests.Unit.CardAssertionTests.CardComponentAssertionTests.When_testing_card_images
{
    [TestFixture]
    public class When_creating_card_image_assertions
    {
        [Test]
        public void Constructor_should_throw_ArgumentNullException_if_card_image_is_null()
        {
            Action act = () => new CardImageAssertions((CardImage)null);
            act.ShouldThrow<ArgumentNullException>();
        }

        [Test]
        public void Constructor_should_throw_ArgumentNullException_if_IHaveAnImage_is_null()
        {
            Action act = () => new CardImageAssertions((IHaveAnImage) null);
            act.ShouldThrow<ArgumentNullException>();
        }
    }
}
