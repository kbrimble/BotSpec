using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BotSpec.Assertions;
using FluentAssertions;
using NSubstitute.Routing.Handlers;
using NUnit.Framework;

namespace BotSpec.Tests.Unit.HelperTests.StringHelperTests
{
    [TestFixture]
    public class When_testing_for_matches
    {
        private StringHelpers<TestException> _sut;
        private static readonly Func<TestException> CreateException = () => new TestException("");

        [SetUp]
        public void SetUp() => _sut = new StringHelpers<TestException>();

        [Test]
        public void TestForMatch_should_throw_ArgumentNullException_when_regex_is_null()
        {
            Action act = () => _sut.TestForMatch("some text", null, CreateException);
            act.ShouldThrow<ArgumentNullException>();
        }

        [Test]
        public void TestForMatch_should_throw_ArgumentNullException_when_exception_func_is_null()
        {
            Action act = () => _sut.TestForMatch("some text", ".*", null);
            act.ShouldThrow<ArgumentNullException>();
        }

        [Test]
        public void TestForMatch_should_call_exception_generation_func_when_throwing()
        {
            var generated = false;
            Func<TestException> func = () =>
            {
                generated = true;
                return new TestException("");
            };
            Action act = () => _sut.TestForMatch(null, ".*", func);
            act.ShouldThrow<TestException>();
            generated.Should().BeTrue();
        }

        [Test]
        public void TestForMatch_should_throw_exception_when_input_is_null()
        {
            Action act = () => _sut.TestForMatch(null, ".*", CreateException);
            act.ShouldThrow<TestException>();
        }

        [Test]
        public void TestForMatch_should_throw_exception_when_regex_does_not_match_input()
        {
            Action act = () => _sut.TestForMatch("some text", "non-matching regex", CreateException);
            act.ShouldThrow<TestException>();
        }

        [Test]
        public void TestForMatch_should_pass_when_regex_matches_input()
        {
            Action act = () => _sut.TestForMatch("some text", ".*", CreateException);
            act.ShouldNotThrow<Exception>();
        }

        [TestCase("some text", "[a-z ]*")]
        [TestCase("SOME TEXT", "[A-Z ]*")]
        [TestCase("SOME TEXT", "[a-z ]*")]
        [TestCase("some text", "[A-Z ]*")]
        public void TestForMatch_should_ignore_case_for_bot_regex_and_input(string input, string regex)
        {
            Action act = () => _sut.TestForMatch(input, regex, CreateException);
            act.ShouldNotThrow<Exception>();
        }
    }
}
