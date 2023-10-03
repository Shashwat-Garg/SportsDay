namespace sportsdayapi.Services
{
    using sportsdayapi.Models.DbModels;

    /// <summary>
    /// Contains methods that do work related to user and event pair
    /// </summary>
    public interface IUserEventService
    {
        /// <summary>
        /// Gets the list of events to which the given user has registered from the database
        /// </summary>
        /// <param name="userId">User id for user whose registered events are needed</param>
        /// <returns>A list of <see cref="Event"/> to which the given user has registered</returns>
        public Task<IEnumerable<Event>> GetEventsForUserAsync(string userId);

        /// <summary>
        /// Registers a user if not already registered for the given event
        /// Adds the <see cref="UserEvent"/> record only if it doesn't already exist in the database
        /// </summary>
        /// <param name="userEvent">User and event data that needs to be added in the database</param>
        /// <returns>Whether the user registration for the event was successful</returns>
        public Task<bool> RegisterUserForEventIfNotRegisteredAsync(UserEvent userEvent);

        /// <summary>
        /// Un-registers a user if they had registered for the given event
        /// Removes the <see cref="UserEvent"/> record only if it exists in the database
        /// </summary>
        /// <param name="userEvent">User and event data that needs to be removed from the database</param>
        /// <returns>Whether the user un-registration for the event was successful</returns>
        public Task<bool> UnRegisterUserForEventAsync(UserEvent userEvent);
    }
}
