using Microsoft.EntityFrameworkCore;
using Moq;
using Moq.EntityFrameworkCore;
using sportsdayapi.Data;
using sportsdayapi.Models.DbModels;

namespace sportsdayapi.UnitTests.MockInitializers
{
    internal static class MockDataContext
    {
        public static DataContext GetFakeDataContext()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                    .UseInMemoryDatabase(databaseName: "FakeDbName")
                    .Options;
            var mockDataContext = new Mock<DataContext>(options);

            mockDataContext.Setup(context => context.Users).ReturnsDbSet(GetFakeUsers());
            mockDataContext.Setup(context => context.Events).ReturnsDbSet(GetFakeEvents());
            mockDataContext.Setup(context => context.UserEvents).ReturnsDbSet(GetFakeUserEvents());
            return mockDataContext.Object;
        }

        public static List<User> GetFakeUsers()
        {
            return new List<User>
            {
                GetFakeUser()
            };
        }

        public static List<Event> GetFakeEvents()
        {
            return new List<Event>
            {
                GetFakeEvent(1),
                GetFakeEvent(2),
            };
        }

        public static List<UserEvent> GetFakeUserEvents(bool withNavigationProperties = true)
        {
            return new List<UserEvent>
            {
                GetFakeUserEvent(withNavigationProperties)
            };
        }

        public static User GetFakeUser()
        {
            return new User { user_id = "test", user_name = "Test user" };
        }

        public static Event GetFakeEvent(int event_id = 1)
        {
            return new Event
            {
                id = event_id,
                event_category = "Test",
                event_name = "Test event",
                start_time = DateTime.Now.AddHours(-2),
                end_time = DateTime.Now.AddHours(-1)
            };
        }

        public static UserEvent GetFakeUserEvent(bool withNavigationProperties = true)
        {
            User fakeUser = GetFakeUser();
            Event fakeEvent = GetFakeEvent();
            return new UserEvent
            {
                user_id = fakeUser.user_id,
                event_id = fakeEvent.id,
                user = withNavigationProperties ? fakeUser : null,
                @event = withNavigationProperties ? fakeEvent : null
            };
        }
    }
}
