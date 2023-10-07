using sportsdayapi.Models.DbModels;
using sportsdayapi.Transformers;

namespace sportsdayapi.UnitTests.TransformersTests
{
    [TestClass]
    public class UserEventTransformerTests
    {
        [TestMethod]
        public void CreateUserEventFromRequest_WithValidUserIdAndEventId()
        {
            string userId = "test";
            int eventId = 1;

            UserEvent userEvent = UserEventTransformer.CreateUserEventFromRequest(userId, eventId);

            // user object should not be null
            Assert.IsNotNull(userEvent);
            
            // user id should be same as what is passed
            Assert.AreEqual(userId, userEvent.user_id);
            
            // event id should be same as what is passed
            Assert.AreEqual(eventId, userEvent.event_id);
        }
    }
}