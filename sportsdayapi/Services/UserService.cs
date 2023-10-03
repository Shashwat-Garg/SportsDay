namespace sportsdayapi.Services
{
    using Microsoft.EntityFrameworkCore;
    using sportsdayapi.Data;
    using sportsdayapi.Models.DbModels;

    /// <inheritdoc/>
    public class UserService : IUserService
    {
        private readonly DataContext _dbContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserService"/> class.
        /// </summary>
        /// <param name="context">The database context</param>
        public UserService(DataContext context)
        {
            this._dbContext = context;
        }

        /// <inheritdoc/>
        public async Task<bool> DoesUserExistAsync(string userId)
        {
            User user = await this.GetUserAsync(userId);
            return user != null;
        }

        /// <inheritdoc/>
        public async Task<bool> AddUserIfNotExistsAsync(User user)
        {
            // If user is already present, can't add the user again
            bool userExists = await this.DoesUserExistAsync(user.user_id);
            if (userExists)
            {
                return false;
            }

            await this.AddUserAsync(user);
            return true;
        }

        /// <inheritdoc/>
        public async Task<User> GetUserAsync(string userId)
        {
            return await this._dbContext.Users.FirstOrDefaultAsync(user => user.user_id == userId);
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<User>> GetUsersAsync(IEnumerable<string> userIds)
        {
            return await this._dbContext.Users
                .Where(user => userIds.Contains(user.user_id))
                .ToListAsync();
        }

        /// <summary>
        /// Adds given user to the database
        /// </summary>
        /// <param name="user">User to be added to the database</param>
        private async Task AddUserAsync(User user)
        {
            await this._dbContext.Users.AddAsync(user);
            await this._dbContext.SaveChangesAsync();
        }
    }
}
