using System;
using System.Collections.Generic;
using BotSpec.Assertions.Cards.CardComponents;
using BotSpec.Tests.Unit.TestData;
using FluentAssertions;
using Microsoft.Bot.Connector.DirectLine;
using NUnit.Framework;

// ReSharper disable ObjectCreationAsStatement

namespace BotSpec.Tests.Unit.CardAssertionTests.CardComponentAssertionTests.When_testing_fact_sets
{
    [TestFixture]
    public class When_constructing_fact_set_assertions
    {
        [Test]
        public void Constructor_should_throw_argument_null_exception_when_facts_are_null()
        {
            Action act = () => new FactSetAssertions((IEnumerable<Fact>)null);
            act.ShouldThrow<ArgumentNullException>();
        }

        [Test]
        public void Only_non_null_facts_from_list_of_Facts_should_be_available()
        {
            var nonNullFacts = FactTestData.CreateRandomFacts();
            var facts = new List<Fact> { null };
            facts.AddRange(nonNullFacts);
            
            var assertions = new FactSetAssertions(facts);

            assertions.Facts.ShouldBeEquivalentTo(nonNullFacts);
        }
    }
}
