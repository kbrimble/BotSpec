using System;
using BotSpec.Assertions.Cards.CardComponents;
using FluentAssertions;
using Microsoft.Bot.Connector.DirectLine;
using NUnit.Framework;

// ReSharper disable ObjectCreationAsStatement

namespace BotSpec.Tests.Unit.CardAssertionTests.CardComponentAssertionTests.When_testing_card_images
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
    }
}
