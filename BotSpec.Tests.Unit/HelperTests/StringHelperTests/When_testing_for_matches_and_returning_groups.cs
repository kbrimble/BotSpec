using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BotSpec.Assertions;
using BotSpec.Exceptions;
using FluentAssertions;
using NUnit.Framework;

namespace BotSpec.Tests.Unit.HelperTests.StringHelperTests
{
    [TestFixture]
    public class When_testing_for_matches_and_returning_groups
    {
        private StringHelpers<TestException> _sut;
        private static readonly Func<TestException> CreateException = () => new TestException("");

        [SetUp]
        public void SetUp() => _sut = new StringHelpers<TestException>();

        [Test]
        public void TestForMatchAndReturnGroups_should_throw_ArgumentNullException_if_regex_is_null()
        {
            Action act = () => _sut.TestForMatchAndReturnGroups("some text", null, "(.*)", CreateException);
            act.ShouldThrow<ArgumentNullException>();
        }

        [Test]
        public void TestForMatchAndReturnGroups_should_throw_ArgumentNullException_if_group_match_regex_is_null()
        {
            Action act = () => _sut.TestForMatchAndReturnGroups("some text", ".*", null, CreateException);
            act.ShouldThrow<ArgumentNullException>();
        }

        [Test]
        public void TestForMatchAndReturnGroups_should_throw_ArgumentNullException_if_exception_generation_func_is_null()
        {
            Action act = () => _sut.TestForMatchAndReturnGroups("some text", ".*", "(.*)", null);
            act.ShouldThrow<ArgumentNullException>();
        }
    }

}
