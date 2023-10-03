namespace sportsdayapi.Services
{
    using Microsoft.EntityFrameworkCore;
    using sportsdayapi.Data;
    using sportsdayapi.Models.DbModels;

    /// <inheritdoc/>
    public class UserEventService : IUserEventService
    {
        private readonly DataContext _dbContext;
        private readonly IUserService _userService;
        private readonly IEventService _eventService;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserEventService"/> class.
        /// </summary>
        /// <param name="context">The database context</param>
        /// <param name="userService">The service for users data</param>
        /// <param name="eventService">The service for events data</param>
        public UserEventService(DataContext context, IUserService userService, IEventService eventService)
        {
            this._dbContext = context;
            this._userService = userService;
            this._eventService = eventService;
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<Event>> GetEventsForUserAsync(string userId)
        {
            return await this._dbContext.UserEvents
                .Where(userEvent => userEvent.user_id == userId)
                .Select(userEvent => userEvent.@event)
                .ToListAsync();
        }

        /// <inheritdoc/>
        public async Task<bool> RegisterUserForEventIfNotRegisteredAsync(UserEvent userEvent)
        {
            bool userEventExists = await this.DoesUserEventExistAsync(userEvent.user_id, userEvent.event_id);

            // User already registered
            if (userEventExists)
            {
                return false;
            }

            // If user is not valid, don't add to db
            bool userExists = await this._userService.DoesUserExistAsync(userEvent.user_id);
            if (!userExists)
            {
                return false;
            }

            // If event is not valid, don't add to db
            bool eventExists = await this._eventService.DoesEventExistAsync(userEvent.event_id);
            if (!eventExists)
            {
                return false;
            }

            // TODO: add validation that user is only able to add max 3 events
            await this.RegisterUserForEventAsync(userEvent);

            return true;
        }

        /// <inheritdoc/>
        public async Task<bool> UnRegisterUserForEventAsync(UserEvent userEvent)
        {
            UserEvent userEventToDelete = await this.GetUserEventAsync(userEvent.user_id, userEvent.event_id);

            // User hasn't registered for the event
            if (userEventToDelete == null)
            {
                return false;
            }

            this._dbContext.UserEvents.Remove(userEventToDelete);
            await this._dbContext.SaveChangesAsync();

            return true;
        }

        /// <summary>
        /// Checks whether the given user id and event id are present in the database as a record
        /// </summary>
        /// <param name="userId">User id for the record</param>
        /// <param name="eventId">Event id for the record</param>
        /// <returns>Whether the record exists</returns>
        private async Task<bool> DoesUserEventExistAsync(string userId, int eventId)
        {
            UserEvent userEvent = await this.GetUserEventAsync(userId, eventId);

            return userEvent != null;
        }

        /// <summary>
        /// Add a user-event record to the database
        /// </summary>
        /// <param name="userEvent">The user-event record to be added</param>
        private async Task RegisterUserForEventAsync(UserEvent userEvent)
        {
            await this._dbContext.UserEvents.AddAsync(userEvent);
            await this._dbContext.SaveChangesAsync();
        }

        /// <summary>
        /// Get the user-event record containing given user id and event id from the database
        /// </summary>
        /// <param name="userId">User id for the record</param>
        /// <param name="eventId">Event id for the record</param>
        /// <returns>The <see cref="UserEvent"/> record containing given user id and event id</returns>
        private async Task<UserEvent> GetUserEventAsync(string userId, int eventId)
        {
            return await this._dbContext.UserEvents
                .FirstOrDefaultAsync(userEvent => userEvent.user_id == userId && userEvent.event_id == eventId);
        }
    }
}
