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
    [TestClass]
    public class UserControllerTests
    {
        private static UserController _userController;

        [ClassInitialize]
        public static void CLassInitialize(TestContext _)
        {
            DataContext mockDataContext = MockDataContext.GetFakeDataContext();
            UserService userService = new UserService(mockDataContext);
            _userController = new UserController(MiscMockObjects.GetFakeLogger<UserController>(), userService);
        }

        [TestMethod]
        public async Task CreateUser_WithNullInput()
        {
            CreateUserRequest createUserRequest = null;

            var result = await _userController.CreateUser(createUserRequest);
            
            var response = result.Result as ObjectResult;
            var createUserResponse = response?.Value as CreateUserResponse;

            // response should not be null
            Assert.IsNotNull(response);
            
            // status code should be BadRequest
            Assert.AreEqual(response.StatusCode, 400);

            // response object should not be null
            Assert.IsNotNull(createUserResponse);
            
            // error message should not be empty
            Assert.IsFalse(string.IsNullOrWhiteSpace(createUserResponse.error_message));
            
            // error message should be invalid input
            Assert.AreEqual(createUserResponse.error_message, "INVALID_INPUT");
            
            // user data should be empty
            Assert.IsNull(createUserResponse.user);
        }

        [TestMethod]
        public async Task CreateUser_WithEmptyUserId()
        {
            CreateUserRequest createUserRequest = new CreateUserRequest
            {
                user_id = string.Empty
            };

            var result = await _userController.CreateUser(createUserRequest);

            var response = result.Result as ObjectResult;
            var createUserResponse = response?.Value as CreateUserResponse;

            // response should not be null
            Assert.IsNotNull(response);

            // status code should be BadRequest
            Assert.AreEqual(response.StatusCode, 400);

            // response object should not be null
            Assert.IsNotNull(createUserResponse);

            // error message should not be empty
            Assert.IsFalse(string.IsNullOrWhiteSpace(createUserResponse.error_message));

            // error message should be invalid input
            Assert.AreEqual(createUserResponse.error_message, "INVALID_INPUT");

            // user data should be empty
            Assert.IsNull(createUserResponse.user);
        }

        [TestMethod]
        public async Task CreateUser_WithValidUserIdEmptyUserName()
        {
            CreateUserRequest createUserRequest = new CreateUserRequest
            {
                user_id = "test",
                user_name = string.Empty
            };

            var result = await _userController.CreateUser(createUserRequest);

            var response = result.Result as ObjectResult;
            var createUserResponse = response?.Value as CreateUserResponse;

            // response should not be null
            Assert.IsNotNull(response);

            // status code should be BadRequest
            Assert.AreEqual(response.StatusCode, 400);

            // response object should not be null
            Assert.IsNotNull(createUserResponse);

            // error message should not be empty
            Assert.IsFalse(string.IsNullOrWhiteSpace(createUserResponse.error_message));

            // error message should be invalid input
            Assert.AreEqual(createUserResponse.error_message, "INVALID_INPUT");

            // user data should be empty
            Assert.IsNull(createUserResponse.user);
        }

        [TestMethod]
        public async Task CreateUser_WithValidUserIdValidUserName()
        {
            CreateUserRequest createUserRequest = new CreateUserRequest
            {
                user_id = "test2",
                user_name = "Test user 2"
            };

            var result = await _userController.CreateUser(createUserRequest);

            var response = result.Result as ObjectResult;
            var createUserResponse = response?.Value as CreateUserResponse;

            // response should not be null
            Assert.IsNotNull(response);

            // status code should be Success
            Assert.AreEqual(response.StatusCode, 200);

            // response object should not be null
            Assert.IsNotNull(createUserResponse);

            // error message should be empty
            Assert.IsTrue(string.IsNullOrWhiteSpace(createUserResponse.error_message));

            // User data should not be empty
            Assert.IsNotNull(createUserResponse.user);

            // User id should be correct
            Assert.AreEqual(createUserResponse.user.user_id, "test2");

            // User name should be correct
            Assert.AreEqual(createUserResponse.user.user_name, "Test user 2");
        }

        [TestMethod]
        public async Task CreateUser_WithValidUserIdValidUserName_DuplicateUser()
        {
            CreateUserRequest createUserRequest = new CreateUserRequest
            {
                user_id = "test",
                user_name = "Test user"
            };

            var result = await _userController.CreateUser(createUserRequest);

            var response = result.Result as ObjectResult;
            var createUserResponse = response?.Value as CreateUserResponse;

            // response should not be null
            Assert.IsNotNull(response);

            // status code should be BadRequest
            Assert.AreEqual(response.StatusCode, 400);

            // response object should not be null
            Assert.IsNotNull(createUserResponse);

            // error message should not be empty
            Assert.IsFalse(string.IsNullOrWhiteSpace(createUserResponse.error_message));

            // error message should say duplicate user
            Assert.AreEqual(createUserResponse.error_message, "DUPLICATE_USER");

            // User data should be empty
            Assert.IsNull(createUserResponse.user);
        }

        [TestMethod]
        public async Task CreateUser_WithValidUserIdValidUserName_ServerError()
        {
            CreateUserRequest createUserRequest = new CreateUserRequest
            {
                user_id = "test",
                user_name = "Test user"
            };

            var logger = MiscMockObjects.GetFakeLogger<UserController>();
            var userService = new Mock<IUserService>();

            // Return any user as a duplicate user
            userService.Setup(service => service.AddUserIfNotExistsAsync(It.IsAny<User>()).Result).Throws(new Exception("Test server error"));
            var controller = new UserController(logger, userService.Object);

            var result = await controller.CreateUser(createUserRequest);

            var response = result.Result as ObjectResult;
            var createUserResponse = response?.Value as CreateUserResponse;

            // response should not be null
            Assert.IsNotNull(response);

            // status code should be InternalServerError
            Assert.AreEqual(response.StatusCode, 500);

            // response object should not be null
            Assert.IsNotNull(createUserResponse);

            // error message should not be empty
            Assert.IsFalse(string.IsNullOrWhiteSpace(createUserResponse.error_message));

            // error message should say server error
            Assert.AreEqual(createUserResponse.error_message, "SERVER_ERROR");

            // User data should be empty
            Assert.IsNull(createUserResponse.user);
        }

        [TestMethod]
        public async Task LoginUser_WithNullInput()
        {
            LoginUserRequest loginUserRequest = null;

            var result = await _userController.LoginUser(loginUserRequest);

            var response = result.Result as ObjectResult;
            var loginUserResponse = response?.Value as LoginUserResponse;

            // response should not be null
            Assert.IsNotNull(response);

            // status code should be BadRequest
            Assert.AreEqual(response.StatusCode, 400);

            // response object should not be null
            Assert.IsNotNull(loginUserResponse);

            // error message should not be empty
            Assert.IsFalse(string.IsNullOrWhiteSpace(loginUserResponse.error_message));

            // error message should be invalid input
            Assert.AreEqual(loginUserResponse.error_message, "INVALID_INPUT");

            // user data should be empty
            Assert.IsNull(loginUserResponse.user);
        }

        [TestMethod]
        public async Task LoginUser_WithEmptyUserId()
        {
            LoginUserRequest loginUserRequest = new LoginUserRequest
            {
                user_id = string.Empty
            };

            var result = await _userController.LoginUser(loginUserRequest);

            var response = result.Result as ObjectResult;
            var loginUserResponse = response?.Value as LoginUserResponse;

            // response should not be null
            Assert.IsNotNull(response);

            // status code should be BadRequest
            Assert.AreEqual(response.StatusCode, 400);

            // response object should not be null
            Assert.IsNotNull(loginUserResponse);

            // error message should not be empty
            Assert.IsFalse(string.IsNullOrWhiteSpace(loginUserResponse.error_message));

            // error message should be invalid input
            Assert.AreEqual(loginUserResponse.error_message, "INVALID_INPUT");

            // user data should be empty
            Assert.IsNull(loginUserResponse.user);
        }

        [TestMethod]
        public async Task LoginUser_WithValidUserIdValidUserName()
        {
            LoginUserRequest loginUserRequest = new LoginUserRequest
            {
                user_id = "test"
            };

            var result = await _userController.LoginUser(loginUserRequest);

            var response = result.Result as ObjectResult;
            var loginUserResponse = response?.Value as LoginUserResponse;

            // response should not be null
            Assert.IsNotNull(response);

            // status code should be Success
            Assert.AreEqual(response.StatusCode, 200);

            // response object should not be null
            Assert.IsNotNull(loginUserResponse);

            // error message should be empty
            Assert.IsTrue(string.IsNullOrWhiteSpace(loginUserResponse.error_message));

            // User data should not be empty
            Assert.IsNotNull(loginUserResponse.user);

            // User id should be correct
            Assert.AreEqual(loginUserResponse.user.user_id, "test");

            // User name should be correct
            Assert.AreEqual(loginUserResponse.user.user_name, "Test user");
        }

        [TestMethod]
        public async Task LoginUser_WithValidUserIdValidUserName_NonExistingUser()
        {
            LoginUserRequest loginUserRequest = new LoginUserRequest
            {
                user_id = "invalid"
            };

            var result = await _userController.LoginUser(loginUserRequest);

            var response = result.Result as ObjectResult;
            var loginUserResponse = response?.Value as LoginUserResponse;

            // response should not be null
            Assert.IsNotNull(response);

            // status code should be BadRequest
            Assert.AreEqual(response.StatusCode, 400);

            // response object should not be null
            Assert.IsNotNull(loginUserResponse);

            // error message should not be empty
            Assert.IsFalse(string.IsNullOrWhiteSpace(loginUserResponse.error_message));

            // error message should say user id is absent
            Assert.AreEqual(loginUserResponse.error_message, "USER_ID_ABSENT");

            // User data should be empty
            Assert.IsNull(loginUserResponse.user);
        }

        [TestMethod]
        public async Task LoginUser_WithValidUserIdValidUserName_ServerError()
        {
            LoginUserRequest loginUserRequest = new LoginUserRequest
            {
                user_id = "test"
            };

            var logger = MiscMockObjects.GetFakeLogger<UserController>();
            var userService = new Mock<IUserService>();

            // Return fakse user as valid user
            userService.Setup(service => service.GetUserAsync(It.IsAny<string>()).Result).Throws(new Exception("Test server error"));
            var controller = new UserController(logger, userService.Object);

            var result = await controller.LoginUser(loginUserRequest);

            var response = result.Result as ObjectResult;
            var loginUserResponse = response?.Value as LoginUserResponse;

            // response should not be null
            Assert.IsNotNull(response);

            // status code should be InternalServerError
            Assert.AreEqual(response.StatusCode, 500);

            // response object should not be null
            Assert.IsNotNull(loginUserResponse);

            // error message should not be empty
            Assert.IsFalse(string.IsNullOrWhiteSpace(loginUserResponse.error_message));

            // error message should say server error
            Assert.AreEqual(loginUserResponse.error_message, "SERVER_ERROR");

            // User data should be empty
            Assert.IsNull(loginUserResponse.user);
        }
    }
}