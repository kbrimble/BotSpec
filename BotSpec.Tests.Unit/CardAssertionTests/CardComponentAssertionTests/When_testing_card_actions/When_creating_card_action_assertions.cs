using System;
using System.Diagnostics.CodeAnalysis;
using BotSpec.Assertions.Cards.CardComponents;
using FluentAssertions;
using NUnit.Framework;

namespace BotSpec.Tests.Unit.CardAssertionTests.CardComponentAssertionTests.When_testing_card_actions
{
    [TestFixture]
    [SuppressMessage("ReSharper", "ObjectCreationAsStatement")]
    public class When_creating_card_action_assertions
    {
        [Test]
        public void Constructor_should_throw_ArgumentNullException_if_card_action_is_null()
        {
            Action act = () => new CardActionAssertions(null);
            act.ShouldThrow<ArgumentNullException>();
        }
    }
}
