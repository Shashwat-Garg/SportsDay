namespace sportsdayapi.Services
{
    using sportsdayapi.Models.DbModels;

    /// <summary>
    /// Contains methods that do work related to users
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// Checks if a user with given user id exists in the database
        /// </summary>
        /// <param name="userId">User id that needs to be checked in the database</param>
        /// <returns>Whether a user with given user id exists in the database</returns>
        public Task<bool> DoesUserExistAsync(string userId);

        /// <summary>
        /// Checks if a user with given user id exists in the database
        /// If not, add the user to the database
        /// </summary>
        /// <param name="user">User that needs to be added to the database</param>
        /// <returns>Whehter the user was successfully added to the database</returns>
        public Task<bool> AddUserIfNotExistsAsync(User user);

        /// <summary>
        /// Get user with given user id from the database
        /// </summary>
        /// <param name="userId">User id needed from the database</param>
        /// <returns>The requested <see cref="User"/> from the database</returns>
        public Task<User> GetUserAsync(string userId);

        /// <summary>
        /// Get users with given user ids from the database
        /// </summary>
        /// <param name="userIds">User ids needed from the database</param>
        /// <returns>A list of <see cref="User"/> from the database</returns>
        public Task<IEnumerable<User>> GetUsersAsync(IEnumerable<string> userIds);
    }
}
