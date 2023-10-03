namespace sportsdayapi.Transformers
{
    using sportsdayapi.Models.DbModels;

    /// <summary>
    /// Contains helper methods regarding <see cref="User"/> model.
    /// </summary>
    public static class UserTransformer
    {
        /// <summary>
        /// Creates <see cref="User"/> from request.
        /// </summary>
        /// <param name="userId">user id</param>
        /// <param name="userName"> user name</param>
        /// <returns>The transformed <see cref="User"/></returns>
        public static User CreateUserFromRequest(string userId, string userName = null)
        {
            return new User { user_id = userId, user_name = userName };
        }
    }
}
