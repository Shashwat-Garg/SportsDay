using Moq;
using sportsdayapi.Data;
using sportsdayapi.Models.DbModels;

namespace sportsdayapi.UnitTests.MockInitializers
{
    internal static class MockDataContext
    {
        public static Mock<DataContext> GetMockDataContext()
        {
            return new Mock<DataContext>();
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
                GetFakeEvent()
            };
        }

        public static List<UserEvent> GetFakeUserEvents()
        {
            return new List<UserEvent>
            {
                GetFakeUserEvent()
            };
        }

        public static User GetFakeUser()
        {
            return new User { user_id = "test", user_name = "Test user" };
        }

        public static Event GetFakeEvent()
        {
            return new Event
            {
                id = 1,
                event_category = "Test",
                event_name = "Test event",
                start_time = DateTime.Now.AddHours(-2),
                end_time = DateTime.Now.AddHours(-1)
            };
        }

        public static UserEvent GetFakeUserEvent()
        {
            User fakeUser = GetFakeUser();
            Event fakeEvent = GetFakeEvent();
            return new UserEvent
            {
                user_id = fakeUser.user_id,
                event_id = fakeEvent.id,
                user = fakeUser,
                @event = fakeEvent
            };
        }
    }
}
