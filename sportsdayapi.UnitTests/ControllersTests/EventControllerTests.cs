using sportsdayapi.Controllers;
using Moq;
using sportsdayapi.Services;
using sportsdayapi.Models;
using Microsoft.AspNetCore.Mvc;
using sportsdayapi.UnitTests.MockInitializers;
using sportsdayapi.Models.DbModels;

namespace sportsdayapi.ControllersTests
{
    [TestClass()]
    public class EventControllerTests
    {
        [TestMethod]
        public async Task GetAllEvents()
        {
            var logger = MiscMockObjects.GetFakeLogger<EventController>();
            var eventService = new Mock<IEventService>();

            // Return all list of events
            eventService.Setup(service => service.GetEventsAsync().Result).Returns(MockDataContext.GetFakeEvents());
            var userEventService = new Mock<IUserEventService>();
            var controller = new EventController(logger.Object, eventService.Object, userEventService.Object);

            var result = await controller.GetEvents();
            
            var response = result.Result as ObjectResult;
            var getEventsResponse = response?.Value as GetEventsResponse;

            // response should not be null
            Assert.IsNotNull(response);
            
            // status code should be Success
            Assert.AreEqual(response.StatusCode, 200);

            // response object should not be null
            Assert.IsNotNull(getEventsResponse);
            
            // error message should be empty
            Assert.IsTrue(string.IsNullOrWhiteSpace(getEventsResponse.error_message));
            
            // events data should not be null
            Assert.IsNotNull(getEventsResponse.events);

            // events data should have only one value
            Assert.AreEqual(getEventsResponse.events.Count(), 1);

            // the event data should be same as fake event data; checking event id to confirm
            Assert.AreEqual(getEventsResponse.events.First().id, MockDataContext.GetFakeEvent().id);
        }

        [TestMethod]
        public async Task GetUserEvents_WithInvalidUser()
        {
            var logger = MiscMockObjects.GetFakeLogger<EventController>();
            var eventService = new Mock<IEventService>();
            var userEventService = new Mock<IUserEventService>();
            
            // Return nothing
            userEventService.Setup(service => service.GetEventsForUserAsync(It.IsAny<string>()).Result).Returns(new List<Event>());
            var controller = new EventController(logger.Object, eventService.Object, userEventService.Object);

            var result = await controller.GetEvents();

            var response = result.Result as ObjectResult;
            var getEventsResponse = response?.Value as GetEventsResponse;

            // response should not be null
            Assert.IsNotNull(response);

            // status code should be Success
            Assert.AreEqual(response.StatusCode, 200);

            // response object should not be null
            Assert.IsNotNull(getEventsResponse);

            // error message should be empty
            Assert.IsTrue(string.IsNullOrWhiteSpace(getEventsResponse.error_message));

            // events data should not be null
            Assert.IsNotNull(getEventsResponse.events);

            // events data should be empty
            Assert.AreEqual(getEventsResponse.events.Count(), 0);
        }

        [TestMethod]
        public async Task GetUserEvents_WithValidUser()
        {
            var logger = MiscMockObjects.GetFakeLogger<EventController>();
            var eventService = new Mock<IEventService>();
            var userEventService = new Mock<IUserEventService>();

            // Return nothing
            userEventService.Setup(service => service.GetEventsForUserAsync(It.IsAny<string>()).Result).Returns(MockDataContext.GetFakeEvents());
            var controller = new EventController(logger.Object, eventService.Object, userEventService.Object);

            var result = await controller.GetEvents("test");

            var response = result.Result as ObjectResult;
            var getEventsResponse = response?.Value as GetEventsResponse;

            // response should not be null
            Assert.IsNotNull(response);

            // status code should be Success
            Assert.AreEqual(response.StatusCode, 200);

            // response object should not be null
            Assert.IsNotNull(getEventsResponse);

            // error message should be empty
            Assert.IsTrue(string.IsNullOrWhiteSpace(getEventsResponse.error_message));

            // events data should not be null
            Assert.IsNotNull(getEventsResponse.events);

            // events data should have only one value
            Assert.AreEqual(getEventsResponse.events.Count(), 1);

            // the event data should be same as fake event data; checking event id to confirm
            Assert.AreEqual(getEventsResponse.events.First().id, MockDataContext.GetFakeEvent().id);
        }

        [TestMethod]
        public async Task RegisterUserForEvent_WithNullRequest()
        {
            RegisterEventRequest registerEventRequest = null;

            var logger = MiscMockObjects.GetFakeLogger<EventController>();
            var eventService = new Mock<IEventService>();
            var userEventService = new Mock<IUserEventService>();
            var controller = new EventController(logger.Object, eventService.Object, userEventService.Object);

            var result = await controller.RegisterUserForEvent(registerEventRequest);

            var response = result.Result as ObjectResult;
            var registerEventResponse = response?.Value as RegisterEventResponse;

            // response should not be null
            Assert.IsNotNull(response);

            // status code should be BadRequest
            Assert.AreEqual(response.StatusCode, 400);

            // response object should not be null
            Assert.IsNotNull(registerEventResponse);

            // error message should not be empty
            Assert.IsFalse(string.IsNullOrWhiteSpace(registerEventResponse.error_message));

            // error message should be invalid input
            Assert.AreEqual(registerEventResponse.error_message, "INVALID_INPUT");

            // request should show failure
            Assert.AreEqual(registerEventResponse.success, false);
        }

        [TestMethod]
        public async Task RegisterUserForEvent_WithEmptyUserId()
        {
            RegisterEventRequest registerEventRequest = new RegisterEventRequest
            {
                user_id = string.Empty
            };

            var logger = MiscMockObjects.GetFakeLogger<EventController>();
            var eventService = new Mock<IEventService>();
            var userEventService = new Mock<IUserEventService>();
            var controller = new EventController(logger.Object, eventService.Object, userEventService.Object);

            var result = await controller.RegisterUserForEvent(registerEventRequest);

            var response = result.Result as ObjectResult;
            var registerEventResponse = response?.Value as RegisterEventResponse;

            // response should not be null
            Assert.IsNotNull(response);

            // status code should be BadRequest
            Assert.AreEqual(response.StatusCode, 400);

            // response object should not be null
            Assert.IsNotNull(registerEventResponse);

            // error message should not be empty
            Assert.IsFalse(string.IsNullOrWhiteSpace(registerEventResponse.error_message));

            // error message should be invalid input
            Assert.AreEqual(registerEventResponse.error_message, "INVALID_INPUT");

            // request should show failure
            Assert.AreEqual(registerEventResponse.success, false);
        }

        [TestMethod]
        public async Task RegisterUserForEvent_WithValidUserIdEmptyEventId()
        {
            RegisterEventRequest registerEventRequest = new RegisterEventRequest
            {
                user_id = "test"
            };

            var logger = MiscMockObjects.GetFakeLogger<EventController>();
            var eventService = new Mock<IEventService>();
            var userEventService = new Mock<IUserEventService>();
            var controller = new EventController(logger.Object, eventService.Object, userEventService.Object);

            var result = await controller.RegisterUserForEvent(registerEventRequest);

            var response = result.Result as ObjectResult;
            var registerEventResponse = response?.Value as RegisterEventResponse;

            // response should not be null
            Assert.IsNotNull(response);

            // status code should be BadRequest
            Assert.AreEqual(response.StatusCode, 400);

            // response object should not be null
            Assert.IsNotNull(registerEventResponse);

            // error message should not be empty
            Assert.IsFalse(string.IsNullOrWhiteSpace(registerEventResponse.error_message));

            // error message should be invalid input
            Assert.AreEqual(registerEventResponse.error_message, "INVALID_INPUT");

            // request should show failure
            Assert.AreEqual(registerEventResponse.success, false);
        }

        [TestMethod]
        public async Task RegisterUserForEvent_WithValidUserIdValidEventId()
        {
            RegisterEventRequest registerEventRequest = new RegisterEventRequest
            {
                user_id = "test",
                event_id = 1
            };

            var logger = MiscMockObjects.GetFakeLogger<EventController>();
            var eventService = new Mock<IEventService>();
            var userEventService = new Mock<IUserEventService>();
            
            // Return true for any register request
            userEventService.Setup(service => service.RegisterUserForEventIfNotRegisteredAsync(It.IsAny<UserEvent>()).Result).Returns(true);
            var controller = new EventController(logger.Object, eventService.Object, userEventService.Object);

            var result = await controller.RegisterUserForEvent(registerEventRequest);

            var response = result.Result as ObjectResult;
            var registerEventResponse = response?.Value as RegisterEventResponse;

            // response should not be null
            Assert.IsNotNull(response);

            // status code should be BadRequest
            Assert.AreEqual(response.StatusCode, 200);

            // response object should not be null
            Assert.IsNotNull(registerEventResponse);

            // error message should be empty
            Assert.IsTrue(string.IsNullOrWhiteSpace(registerEventResponse.error_message));

            // request should show success
            Assert.AreEqual(registerEventResponse.success, true);
        }

        [TestMethod]
        public async Task RegisterUserForEvent_WithValidUserIdValidEventId_InvalidData()
        {
            RegisterEventRequest registerEventRequest = new RegisterEventRequest
            {
                user_id = "test",
                event_id = 1
            };

            var logger = MiscMockObjects.GetFakeLogger<EventController>();
            var eventService = new Mock<IEventService>();
            var userEventService = new Mock<IUserEventService>();

            // Return false for any register request
            userEventService.Setup(service => service.RegisterUserForEventIfNotRegisteredAsync(It.IsAny<UserEvent>()).Result).Returns(false);
            var controller = new EventController(logger.Object, eventService.Object, userEventService.Object);

            var result = await controller.RegisterUserForEvent(registerEventRequest);

            var response = result.Result as ObjectResult;
            var registerEventResponse = response?.Value as RegisterEventResponse;

            // response should not be null
            Assert.IsNotNull(response);

            // status code should be BadRequest
            Assert.AreEqual(response.StatusCode, 400);

            // response object should not be null
            Assert.IsNotNull(registerEventResponse);

            // error message should not be empty
            Assert.IsFalse(string.IsNullOrWhiteSpace(registerEventResponse.error_message));

            // error message should be invalid input
            Assert.AreEqual(registerEventResponse.error_message, "INVALID_DATA");

            // request should show failure
            Assert.AreEqual(registerEventResponse.success, false);
        }

        [TestMethod]
        public async Task UnRegisterUserForEvent_WithNullRequest()
        {
            RegisterEventRequest registerEventRequest = null;

            var logger = MiscMockObjects.GetFakeLogger<EventController>();
            var eventService = new Mock<IEventService>();
            var userEventService = new Mock<IUserEventService>();
            var controller = new EventController(logger.Object, eventService.Object, userEventService.Object);

            var result = await controller.UnRegisterUserForEvent(registerEventRequest);

            var response = result.Result as ObjectResult;
            var registerEventResponse = response?.Value as RegisterEventResponse;

            // response should not be null
            Assert.IsNotNull(response);

            // status code should be BadRequest
            Assert.AreEqual(response.StatusCode, 400);

            // response object should not be null
            Assert.IsNotNull(registerEventResponse);

            // error message should not be empty
            Assert.IsFalse(string.IsNullOrWhiteSpace(registerEventResponse.error_message));

            // error message should be invalid input
            Assert.AreEqual(registerEventResponse.error_message, "INVALID_INPUT");

            // request should show failure
            Assert.AreEqual(registerEventResponse.success, false);
        }

        [TestMethod]
        public async Task UnRegisterUserForEvent_WithEmptyUserId()
        {
            RegisterEventRequest registerEventRequest = new RegisterEventRequest
            {
                user_id = string.Empty
            };

            var logger = MiscMockObjects.GetFakeLogger<EventController>();
            var eventService = new Mock<IEventService>();
            var userEventService = new Mock<IUserEventService>();
            var controller = new EventController(logger.Object, eventService.Object, userEventService.Object);

            var result = await controller.UnRegisterUserForEvent(registerEventRequest);

            var response = result.Result as ObjectResult;
            var registerEventResponse = response?.Value as RegisterEventResponse;

            // response should not be null
            Assert.IsNotNull(response);

            // status code should be BadRequest
            Assert.AreEqual(response.StatusCode, 400);

            // response object should not be null
            Assert.IsNotNull(registerEventResponse);

            // error message should not be empty
            Assert.IsFalse(string.IsNullOrWhiteSpace(registerEventResponse.error_message));

            // error message should be invalid input
            Assert.AreEqual(registerEventResponse.error_message, "INVALID_INPUT");

            // request should show failure
            Assert.AreEqual(registerEventResponse.success, false);
        }

        [TestMethod]
        public async Task UnRegisterUserForEvent_WithValidUserIdEmptyEventId()
        {
            RegisterEventRequest registerEventRequest = new RegisterEventRequest
            {
                user_id = "test"
            };

            var logger = MiscMockObjects.GetFakeLogger<EventController>();
            var eventService = new Mock<IEventService>();
            var userEventService = new Mock<IUserEventService>();
            var controller = new EventController(logger.Object, eventService.Object, userEventService.Object);

            var result = await controller.UnRegisterUserForEvent(registerEventRequest);

            var response = result.Result as ObjectResult;
            var registerEventResponse = response?.Value as RegisterEventResponse;

            // response should not be null
            Assert.IsNotNull(response);

            // status code should be BadRequest
            Assert.AreEqual(response.StatusCode, 400);

            // response object should not be null
            Assert.IsNotNull(registerEventResponse);

            // error message should not be empty
            Assert.IsFalse(string.IsNullOrWhiteSpace(registerEventResponse.error_message));

            // error message should be invalid input
            Assert.AreEqual(registerEventResponse.error_message, "INVALID_INPUT");

            // request should show failure
            Assert.AreEqual(registerEventResponse.success, false);
        }

        [TestMethod]
        public async Task UnRegisterUserForEvent_WithValidUserIdValidEventId()
        {
            RegisterEventRequest registerEventRequest = new RegisterEventRequest
            {
                user_id = "test",
                event_id = 1
            };

            var logger = MiscMockObjects.GetFakeLogger<EventController>();
            var eventService = new Mock<IEventService>();
            var userEventService = new Mock<IUserEventService>();

            // Return true for any register request
            userEventService.Setup(service => service.UnRegisterUserForEventAsync(It.IsAny<UserEvent>()).Result).Returns(true);
            var controller = new EventController(logger.Object, eventService.Object, userEventService.Object);

            var result = await controller.UnRegisterUserForEvent(registerEventRequest);

            var response = result.Result as ObjectResult;
            var registerEventResponse = response?.Value as RegisterEventResponse;

            // response should not be null
            Assert.IsNotNull(response);

            // status code should be BadRequest
            Assert.AreEqual(response.StatusCode, 200);

            // response object should not be null
            Assert.IsNotNull(registerEventResponse);

            // error message should be empty
            Assert.IsTrue(string.IsNullOrWhiteSpace(registerEventResponse.error_message));

            // request should show success
            Assert.AreEqual(registerEventResponse.success, true);
        }

        [TestMethod]
        public async Task UnRegisterUserForEvent_WithValidUserIdValidEventId_InvalidData()
        {
            RegisterEventRequest registerEventRequest = new RegisterEventRequest
            {
                user_id = "test",
                event_id = 1
            };

            var logger = MiscMockObjects.GetFakeLogger<EventController>();
            var eventService = new Mock<IEventService>();
            var userEventService = new Mock<IUserEventService>();

            // Return false for any register request
            userEventService.Setup(service => service.UnRegisterUserForEventAsync(It.IsAny<UserEvent>()).Result).Returns(false);
            var controller = new EventController(logger.Object, eventService.Object, userEventService.Object);

            var result = await controller.UnRegisterUserForEvent(registerEventRequest);

            var response = result.Result as ObjectResult;
            var registerEventResponse = response?.Value as RegisterEventResponse;

            // response should not be null
            Assert.IsNotNull(response);

            // status code should be BadRequest
            Assert.AreEqual(response.StatusCode, 400);

            // response object should not be null
            Assert.IsNotNull(registerEventResponse);

            // error message should not be empty
            Assert.IsFalse(string.IsNullOrWhiteSpace(registerEventResponse.error_message));

            // error message should be invalid input
            Assert.AreEqual(registerEventResponse.error_message, "INVALID_DATA");

            // request should show failure
            Assert.AreEqual(registerEventResponse.success, false);
        }
    }
}