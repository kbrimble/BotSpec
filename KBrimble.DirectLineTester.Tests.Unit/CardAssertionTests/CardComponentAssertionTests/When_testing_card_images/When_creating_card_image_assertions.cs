using System;
using FluentAssertions;
using KBrimble.DirectLineTester.Assertions.Cards.CardComponents;
using NUnit.Framework;

namespace KBrimble.DirectLineTester.Tests.Unit.CardAssertionTests.CardComponentAssertionTests.When_testing_card_images
{
    [TestFixture]
    public class When_creating_card_image_assertions
    {
        [Test]
        public void Constructor_should_throw_ArgumentNullException_if_card_image_is_null()
        {
            Action act = () => new CardImageAssertions(null);
            act.ShouldThrow<ArgumentNullException>();
        }
    }
}
