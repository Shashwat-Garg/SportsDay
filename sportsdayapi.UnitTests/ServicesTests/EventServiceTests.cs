using sportsdayapi.Services;
using sportsdayapi.UnitTests.MockInitializers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sportsdayapi.UnitTests.ServicesTests
{
    [TestClass]
    public class EventServiceTests
    {
        private static EventService _eventService;

        [ClassInitialize]
        public static void ClassInitialize(TestContext _)
        {
            var dbContext = MockDataContext.GetFakeDataContext();
            _eventService = new EventService(dbContext);
        }

        [TestMethod]
        public async Task GetEventsAsync_InvalidEventIds()
        {
            IEnumerable<int> eventIds = new List<int>();

            var events = await _eventService.GetEventsAsync(eventIds);

            Assert.IsNotNull(events);

            Assert.AreEqual(0, events.Count());
        }

        [TestMethod]
        public async Task GetEventsAsync_ValidEventIds()
        {
            IEnumerable<int> eventIds = new List<int> { 1 };

            var events = await _eventService.GetEventsAsync(eventIds);

            Assert.IsNotNull(events);

            Assert.AreEqual(1, events.Count());

            Assert.AreEqual(1, events.First().id);
        }
    }
}
