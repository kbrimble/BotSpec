using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using FluentAssertions;
using KBrimble.DirectLineTester.Assertions.Cards.CardComponents;
using KBrimble.DirectLineTester.Models.Cards;
using KBrimble.DirectLineTester.Tests.Unit.TestData;
using NUnit.Framework;
// ReSharper disable ObjectCreationAsStatement

namespace KBrimble.DirectLineTester.Tests.Unit.CardAssertionTests.CardComponentAssertionTests.When_testing_fact_sets
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
        public void Constructor_should_throw_ArgumentNullException_when_IHaveFacts_is_null()
        {
            Action act = () => new FactSetAssertions((IHaveFacts)null);
            act.ShouldThrow<ArgumentNullException>();
        }

        [Test]
        public void Constructor_should_throw_ArgumentNullException_when_Facts_property_of_IHaveFacts_is_null()
        {
            IHaveFacts iHaveFacts = new ReceiptCard(facts: null);
            Action act = () => new FactSetAssertions(iHaveFacts);
            act.ShouldThrow<ArgumentNullException>();
        }

        [Test]
        public void Constructor_should_throw_ArgumentNullException_when_list_of_IHaveFacts_is_null()
        {
            Action act = () => new FactSetAssertions((IEnumerable<IHaveFacts>)null);
            act.ShouldThrow<ArgumentNullException>();
        }

        [Test]
        public void Only_non_null_facts_from_IHaveFacts_should_be_available()
        {
            var nonNullFacts = FactTestData.CreateRandomFacts();
            var facts = new List<Fact> { null };
            facts.AddRange(nonNullFacts);
            IHaveFacts iHaveFacts = new ReceiptCard(facts: facts);
            
            var assertions = new FactSetAssertions(iHaveFacts);

            assertions.Facts.ShouldBeEquivalentTo(nonNullFacts);
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

        [Test]
        [SuppressMessage("ReSharper", "PossibleMultipleEnumeration")]
        public void Only_non_null_facts_from_list_of_IHaveFacts_should_be_available()
        {
            var facts = FactTestData.CreateRandomFacts();
            var nonNullIHaveFacts = ReceiptCardTestData.CreateReceiptCardSetWithAllCardsWithSetProperties(facts: facts);
            var iHaveFacts = new List<IHaveFacts> { null };
            iHaveFacts.AddRange(nonNullIHaveFacts);
            var inputList = iHaveFacts.Cast<IHaveFacts>();

            var sut = new FactSetAssertions(inputList);

            sut.Facts.ShouldBeEquivalentTo(nonNullIHaveFacts.SelectMany(x => x.Facts));
        }
    }
}
