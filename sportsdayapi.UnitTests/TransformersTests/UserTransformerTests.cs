using sportsdayapi.Models.DbModels;
using sportsdayapi.Transformers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sportsdayapi.UnitTests.TransformersTests
{
    [TestClass]
    public class UserTransformerTests
    {
        [TestMethod]
        public void CreateUserFromRequest_WithValidUserIdAndEmptyUserName()
        {
            string userId = "test";

            User user = UserTransformer.CreateUserFromRequest(userId);

            // user object should not be null
            Assert.IsNotNull(user);
            
            // user id should be same as what is passed
            Assert.AreEqual(userId, user.user_id);
            
            // user name should be empty
            Assert.IsTrue(string.IsNullOrWhiteSpace(user.user_name));
        }

        [TestMethod]
        public void CreateUserFromRequest_WithValidUserIdAndValidUserName()
        {
            string userId = "test";
            string userName = "Test user";

            User user = UserTransformer.CreateUserFromRequest(userId, userName);

            // user object should not be null
            Assert.IsNotNull(user);

            // user id should be same as what is passed
            Assert.AreEqual(userId, user.user_id);

            // user name should be same as what is passed
            Assert.AreEqual(userName, user.user_name);
        }
    }
}
