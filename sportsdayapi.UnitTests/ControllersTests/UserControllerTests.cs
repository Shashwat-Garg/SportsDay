using sportsdayapi.Controllers;
using Microsoft.Extensions.Logging;
using Moq;
using sportsdayapi.Services;
using sportsdayapi.Models;
using Microsoft.AspNetCore.Mvc;
using sportsdayapi.UnitTests.MockInitializers;
using sportsdayapi.Models.DbModels;

namespace sportsdayapi.ControllersTests
{
    [TestClass]
    public class UserControllerTests
    {
        [TestMethod]
        public async Task CreateUser_WithNullInput()
        {
            var logger = MiscMockObjects.GetFakeLogger<UserController>();
            var userService = new Mock<IUserService>();
            var controller = new UserController(logger.Object, userService.Object);

            CreateUserRequest createUserRequest = null;

            var result = await controller.CreateUser(createUserRequest);
            
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
            var logger = MiscMockObjects.GetFakeLogger<UserController>();
            var userService = new Mock<IUserService>();
            var controller = new UserController(logger.Object, userService.Object);

            CreateUserRequest createUserRequest = new CreateUserRequest
            {
                user_id = string.Empty
            };

            var result = await controller.CreateUser(createUserRequest);

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
            var logger = MiscMockObjects.GetFakeLogger<UserController>();
            var userService = new Mock<IUserService>();
            var controller = new UserController(logger.Object, userService.Object);

            CreateUserRequest createUserRequest = new CreateUserRequest
            {
                user_id = "test",
                user_name = string.Empty
            };

            var result = await controller.CreateUser(createUserRequest);

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
                user_id = "test",
                user_name = "Test user"
            };

            var logger = MiscMockObjects.GetFakeLogger<UserController>();
            var userService = new Mock<IUserService>();
            
            // Add any user as valid user
            userService.Setup(service => service.AddUserIfNotExistsAsync(It.IsAny<User>()).Result).Returns(true);
            var controller = new UserController(logger.Object, userService.Object);

            var result = await controller.CreateUser(createUserRequest);

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
            Assert.AreEqual(createUserResponse.user.user_id, "test");

            // User name should be correct
            Assert.AreEqual(createUserResponse.user.user_name, "Test user");
        }

        [TestMethod]
        public async Task CreateUser_WithValidUserIdValidUserName_DuplicateUser()
        {
            CreateUserRequest createUserRequest = new CreateUserRequest
            {
                user_id = "test",
                user_name = "Test user"
            };

            var logger = MiscMockObjects.GetFakeLogger<UserController>();
            var userService = new Mock<IUserService>();
            
            // Return any user as a duplicate user
            userService.Setup(service => service.AddUserIfNotExistsAsync(It.IsAny<User>()).Result).Returns(false);
            var controller = new UserController(logger.Object, userService.Object);

            var result = await controller.CreateUser(createUserRequest);

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

            Assert.AreEqual(createUserResponse.error_message, "DUPLICATE_USER");

            // User data should be empty
            Assert.IsNull(createUserResponse.user);
        }

        [TestMethod]
        public async Task LoginUser_WithNullInput()
        {
            var logger = MiscMockObjects.GetFakeLogger<UserController>();
            var userService = new Mock<IUserService>();
            var controller = new UserController(logger.Object, userService.Object);

            LoginUserRequest loginUserRequest = null;

            var result = await controller.LoginUser(loginUserRequest);

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
            var logger = MiscMockObjects.GetFakeLogger<UserController>();
            var userService = new Mock<IUserService>();
            var controller = new UserController(logger.Object, userService.Object);

            LoginUserRequest loginUserRequest = new LoginUserRequest
            {
                user_id = string.Empty
            };

            var result = await controller.LoginUser(loginUserRequest);

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

            var logger = MiscMockObjects.GetFakeLogger<UserController>();
            var userService = new Mock<IUserService>();

            // Return fakse user as valid user
            userService.Setup(service => service.GetUserAsync(It.IsAny<string>()).Result).Returns(MockDataContext.GetFakeUser());
            var controller = new UserController(logger.Object, userService.Object);

            var result = await controller.LoginUser(loginUserRequest);

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
                user_id = "test"
            };

            var logger = MiscMockObjects.GetFakeLogger<UserController>();
            var userService = new Mock<IUserService>();

            // Return fakse user as valid user
            userService.Setup(service => service.GetUserAsync(It.IsAny<string>()).Result).Returns<IUserService, User>(null);
            var controller = new UserController(logger.Object, userService.Object);

            var result = await controller.LoginUser(loginUserRequest);

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

            Assert.AreEqual(loginUserResponse.error_message, "USER_ID_ABSENT");

            // User data should be empty
            Assert.IsNull(loginUserResponse.user);
        }
    }
}