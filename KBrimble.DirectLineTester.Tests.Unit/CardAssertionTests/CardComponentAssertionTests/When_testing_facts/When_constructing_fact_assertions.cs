using System;
using FluentAssertions;
using KBrimble.DirectLineTester.Assertions.Cards.CardComponents;
using NUnit.Framework;
// ReSharper disable ObjectCreationAsStatement

namespace KBrimble.DirectLineTester.Tests.Unit.CardAssertionTests.CardComponentAssertionTests.When_testing_facts
{
    [TestFixture]
    public class When_constructing_fact_assertions
    {
        [Test]
        public void Constructor_should_throw_ArgumentNullException_when_Fact_is_null()
        {
            Action act = () => new FactAssertions(null);
            act.ShouldThrow<ArgumentNullException>();
        }
    }
}
