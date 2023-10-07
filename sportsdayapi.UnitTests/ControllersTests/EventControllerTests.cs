using sportsdayapi.Controllers;
using Moq;
using sportsdayapi.Services;
using sportsdayapi.Models;
using Microsoft.AspNetCore.Mvc;
using sportsdayapi.UnitTests.MockInitializers;
using sportsdayapi.Models.DbModels;
using sportsdayapi.Data;

namespace sportsdayapi.ControllersTests
{
    [TestClass()]
    public class EventControllerTests
    {
        private static EventController _eventController;

        [ClassInitialize]
        public static void ClassInitialize(TestContext _)
        {
            DataContext mockDataContext = MockDataContext.GetFakeDataContext();
            EventService eventService = new EventService(mockDataContext);
            UserService userService = new UserService(mockDataContext);
            UserEventService userEventService = new UserEventService(mockDataContext, userService, eventService);
            _eventController = new EventController(MiscMockObjects.GetFakeLogger<EventController>(), eventService, userEventService);
        }

        [TestMethod]
        public async Task GetAllEvents()
        {
            // Return all list of events
            var result = await _eventController.GetEvents();
            
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
            Assert.AreEqual(getEventsResponse.events.Count(), 2);
        }

        [TestMethod]
        public async Task GetUserEvents_WithInvalidUser()
        {
            string userId = "test2";

            var result = await _eventController.GetEvents(userId);

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
            string userId = "test";

            var result = await _eventController.GetEvents(userId);

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
        public async Task GetUserEvents_WithValidUser_ServerError()
        {
            var logger = MiscMockObjects.GetFakeLogger<EventController>();
            var eventService = new Mock<IEventService>();
            var userEventService = new Mock<IUserEventService>();

            // Return nothing
            userEventService.Setup(service => service.GetEventsForUserAsync(It.IsAny<string>()).Result).Throws(new Exception("Test server error"));
            var controller = new EventController(logger, eventService.Object, userEventService.Object);

            var result = await controller.GetEvents("test");

            var response = result.Result as ObjectResult;
            var getEventsResponse = response?.Value as GetEventsResponse;

            // response should not be null
            Assert.IsNotNull(response);

            // status code should be InternalServerError
            Assert.AreEqual(response.StatusCode, 500);

            // response object should not be null
            Assert.IsNotNull(getEventsResponse);

            // error message should not be empty
            Assert.IsFalse(string.IsNullOrWhiteSpace(getEventsResponse.error_message));

            // error message should say server error
            Assert.AreEqual(getEventsResponse.error_message, "SERVER_ERROR");

            // events data should be null
            Assert.IsNull(getEventsResponse.events);
        }

        [TestMethod]
        public async Task RegisterUserForEvent_WithNullRequest()
        {
            RegisterEventRequest registerEventRequest = null;

            var result = await _eventController.RegisterUserForEvent(registerEventRequest);

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

            var result = await _eventController.RegisterUserForEvent(registerEventRequest);

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

            var result = await _eventController.RegisterUserForEvent(registerEventRequest);

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
                event_id = 2
            };

            var result = await _eventController.RegisterUserForEvent(registerEventRequest);

            var response = result.Result as ObjectResult;
            var registerEventResponse = response?.Value as RegisterEventResponse;

            // response should not be null
            Assert.IsNotNull(response);

            // status code should be Success
            Assert.AreEqual(response.StatusCode, 200);

            // response object should not be null
            Assert.IsNotNull(registerEventResponse);

            // error message should be empty
            Assert.IsTrue(string.IsNullOrWhiteSpace(registerEventResponse.error_message));

            // request should show success
            Assert.AreEqual(registerEventResponse.success, true);
        }

        [TestMethod]
        public async Task RegisterUserForEvent_WithValidUserIdValidEventId_UserNotExists()
        {
            RegisterEventRequest registerEventRequest = new RegisterEventRequest
            {
                user_id = "invalid",
                event_id = 1
            };

            var result = await _eventController.RegisterUserForEvent(registerEventRequest);

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
        public async Task RegisterUserForEvent_WithValidUserIdValidEventId_EventNotExists()
        {
            RegisterEventRequest registerEventRequest = new RegisterEventRequest
            {
                user_id = "test",
                event_id = 100
            };

            var result = await _eventController.RegisterUserForEvent(registerEventRequest);

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
        public async Task RegisterUserForEvent_WithValidUserIdValidEventId_DuplicateRegistration()
        {
            RegisterEventRequest registerEventRequest = new RegisterEventRequest
            {
                user_id = "test",
                event_id = 1
            };

            var result = await _eventController.RegisterUserForEvent(registerEventRequest);

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
        public async Task RegisterUserForEvent_WithValidUserIdValidEventId_ServerError()
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
            userEventService.Setup(service => service.RegisterUserForEventIfNotRegisteredAsync(It.IsAny<UserEvent>()).Result).Throws(new Exception("Test server error"));
            var controller = new EventController(logger, eventService.Object, userEventService.Object);

            var result = await controller.RegisterUserForEvent(registerEventRequest);

            var response = result.Result as ObjectResult;
            var registerEventResponse = response?.Value as RegisterEventResponse;

            // response should not be null
            Assert.IsNotNull(response);

            // status code should be InternalServerError
            Assert.AreEqual(response.StatusCode, 500);

            // response object should not be null
            Assert.IsNotNull(registerEventResponse);

            // error message should not be empty
            Assert.IsFalse(string.IsNullOrWhiteSpace(registerEventResponse.error_message));

            // error message should be server error
            Assert.AreEqual(registerEventResponse.error_message, "SERVER_ERROR");

            // request should show failure
            Assert.AreEqual(registerEventResponse.success, false);
        }

        [TestMethod]
        public async Task UnRegisterUserForEvent_WithNullRequest()
        {
            RegisterEventRequest registerEventRequest = null;

            var result = await _eventController.UnRegisterUserForEvent(registerEventRequest);

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

            var result = await _eventController.UnRegisterUserForEvent(registerEventRequest);

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

            var result = await _eventController.UnRegisterUserForEvent(registerEventRequest);

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

            var result = await _eventController.UnRegisterUserForEvent(registerEventRequest);

            var response = result.Result as ObjectResult;
            var registerEventResponse = response?.Value as RegisterEventResponse;

            // response should not be null
            Assert.IsNotNull(response);

            // status code should be Success
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
                event_id = 2
            };

            var result = await _eventController.UnRegisterUserForEvent(registerEventRequest);

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
        public async Task UnRegisterUserForEvent_WithValidUserIdValidEventId_ServerError()
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
            userEventService.Setup(service => service.UnRegisterUserForEventAsync(It.IsAny<UserEvent>()).Result).Throws(new Exception("Test server error"));
            var controller = new EventController(logger, eventService.Object, userEventService.Object);

            var result = await controller.UnRegisterUserForEvent(registerEventRequest);

            var response = result.Result as ObjectResult;
            var registerEventResponse = response?.Value as RegisterEventResponse;

            // response should not be null
            Assert.IsNotNull(response);

            // status code should be InternalServerError
            Assert.AreEqual(response.StatusCode, 500);

            // response object should not be null
            Assert.IsNotNull(registerEventResponse);

            // error message should not be empty
            Assert.IsFalse(string.IsNullOrWhiteSpace(registerEventResponse.error_message));

            // error message should be invalid input
            Assert.AreEqual(registerEventResponse.error_message, "SERVER_ERROR");

            // request should show failure
            Assert.AreEqual(registerEventResponse.success, false);
        }
    }
}