using sportsdayapi.Models.DbModels;
using sportsdayapi.Services;
using sportsdayapi.UnitTests.MockInitializers;

namespace sportsdayapi.UnitTests.ServicesTests
{
    [TestClass]
    public class UserServiceTests
    {
        private static UserService _userService;

        [ClassInitialize]
        public static void ClassInitialize(TestContext _)
        {
            var mockDataContext = MockDataContext.GetFakeDataContext();
            _userService = new UserService(mockDataContext);
        }

        [TestMethod]
        public async Task GetUsersAsync_InvalidUserIds()
        {
            IEnumerable<string> userIds = new List<string>();

            IEnumerable<User> users = await _userService.GetUsersAsync(userIds);

            Assert.AreEqual(0, users.Count());
        }

        [TestMethod]
        public async Task GetUsersAsync_ValidUserIds()
        {
            IEnumerable<string> userIds = new List<string> { "test" };

            IEnumerable<User> users = await _userService.GetUsersAsync(userIds);

            Assert.AreEqual(1, users.Count());

            Assert.AreEqual("test", users.First().user_id);
        }
    }
}
