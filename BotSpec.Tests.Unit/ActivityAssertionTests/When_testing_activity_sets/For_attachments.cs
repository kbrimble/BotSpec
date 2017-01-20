using BotSpec.Assertions.Activities;
using BotSpec.Assertions.Attachments;
using BotSpec.Tests.Unit.TestData;
using FluentAssertions;
using NUnit.Framework;

namespace BotSpec.Tests.Unit.ActivityAssertionTests.When_testing_activity_sets
{
    [TestFixture]
    public class For_attachments
    {
        [Test]
        public void Attachment_should_return_ActivitySetAttachmentAssertions()
        {
            var activities = ActivityTestData.CreateRandomActivities();

            var sut = new ActivitySetAssertions(activities);

            sut.WithAttachment().Should().BeAssignableTo<ActivitySetAttachmentAssertions>().And.NotBeNull();
        }
    }
}
