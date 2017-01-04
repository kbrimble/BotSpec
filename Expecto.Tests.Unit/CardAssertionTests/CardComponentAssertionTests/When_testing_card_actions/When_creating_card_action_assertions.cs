using System;
using Expecto.Assertions.Cards.CardComponents;
using FluentAssertions;
using NUnit.Framework;

namespace Expecto.Tests.Unit.CardAssertionTests.CardComponentAssertionTests.When_testing_card_actions
{
    [TestFixture]
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
