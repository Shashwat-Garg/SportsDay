using Microsoft.EntityFrameworkCore;
using sportsdayapi.Data;
using sportsdayapi.UnitTests.MockInitializers;

namespace sportsdayapi.UnitTests.DataContextTests
{
    [TestClass]
    public class DataContextTests
    {
        [TestMethod]

        public void TestModelCreationOfData_WithoutInitialEvents()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                    .UseInMemoryDatabase(databaseName: "FakeDbName1")
                    .Options;

            var dataContext = new DataContext(options);
            
            // Initialize data context
            DataInitializer.Initialize(dataContext);

            Assert.IsNotNull(dataContext);

            Assert.AreEqual(10, dataContext.Events.Count());
        }

        [TestMethod]
        public void TestModelCreationOfData_WithInitialEvents()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                    .UseInMemoryDatabase(databaseName: "FakeDbName2")
                    .Options;

            var dataContext = new DataContext(options);

            var fakeEvents = MockDataContext.GetFakeEvents();

            dataContext.Database.EnsureCreated();

            // Add some mock events
            dataContext.Events.AddRange(fakeEvents);

            dataContext.SaveChanges();

            // Now when we initialize, the events will not be overridden
            DataInitializer.Initialize(dataContext);

            Assert.IsNotNull(dataContext);

            Assert.AreEqual(fakeEvents.Count(), dataContext.Events.Count());
        }

        [TestMethod]
        public void TestUserAndEventsNavigationProperties()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                    .UseInMemoryDatabase(databaseName: "FakeDbName3")
                    .EnableSensitiveDataLogging(true)
                    .Options;

            var dataContext = new DataContext(options);

            var fakeEvents = MockDataContext.GetFakeEvents();
            var fakeUsers = MockDataContext.GetFakeUsers();
            var fakeUserEvents = MockDataContext.GetFakeUserEvents(false);
            string userId = fakeUsers.First().user_id;
            int userEventId = fakeUserEvents.First(userEvent => userEvent.user_id == userId).event_id;
            int eventId = fakeEvents.First().id;
            string eventUserId = fakeUserEvents.First(userEvent => userEvent.event_id == eventId).user_id;

            dataContext.Database.EnsureCreated();

            // Add mock data
            dataContext.Events.AddRange(fakeEvents);
            dataContext.Users.AddRange(fakeUsers);
            dataContext.UserEvents.AddRange(fakeUserEvents);

            dataContext.SaveChanges();

            Assert.IsNotNull(dataContext);

            // Ensure that the data context has valid data
            Assert.AreEqual(fakeEvents.Count(), dataContext.Events.Count());
            Assert.AreEqual(fakeUsers.Count(), dataContext.Users.Count());
            Assert.AreEqual(fakeUserEvents.Count(), dataContext.UserEvents.Count());

            // Ensure that the navigation properties are working correctly
            Assert.AreEqual(userEventId, dataContext.Users.First().user_events.First().event_id);
            Assert.AreEqual(eventUserId, dataContext.Events.First().user_events.First().user_id);
        }
    }
}