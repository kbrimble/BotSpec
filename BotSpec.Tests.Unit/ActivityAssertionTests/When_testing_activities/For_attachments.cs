using BotSpec.Assertions.Activities;
using BotSpec.Assertions.Attachments;
using FluentAssertions;
using Microsoft.Bot.Connector.DirectLine;
using NUnit.Framework;

namespace BotSpec.Tests.Unit.ActivityAssertionTests.When_testing_activities
{
    [TestFixture]
    public class For_attachments
    {
        [Test]
        public void Attachment_should_return_ActivityAttachmentAssertions()
        {
            var activity = new Activity();

            var sut = new ActivityAssertions(activity);

            sut.WithAttachment().Should().BeAssignableTo<ActivityAttachmentAssertions>().And.NotBeNull();
        }
    }
}
