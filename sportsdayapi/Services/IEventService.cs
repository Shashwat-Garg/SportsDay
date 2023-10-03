namespace sportsdayapi.Services
{
    using sportsdayapi.Models.DbModels;

    /// <summary>
    /// Contains methods that do work related to events
    /// </summary>
    public interface IEventService
    {
        /// <summary>
        /// Checks if an event with given event id exists in the database
        /// </summary>
        /// <param name="eventId">Id for event that needs to be checked</param>
        /// <returns>Whether the event exists in the database</returns>
        public Task<bool> DoesEventExistAsync(int eventId);

        /// <summary>
        /// Get the event with given event id from database
        /// </summary>
        /// <param name="eventId">Id for event needed</param>
        /// <returns>The requested <see cref="Event"/></returns>
        public Task<Event> GetEventAsync(int eventId);

        /// <summary>
        /// Get the list of all events from the database
        /// </summary>
        /// <returns>A list of <see cref="Event"/></returns>
        public Task<IEnumerable<Event>> GetEventsAsync();

        /// <summary>
        /// Get the data for events with given ids from the database
        /// </summary>
        /// <param name="eventIds">Event ids whose data is needed</param>
        /// <returns>A list of <see cref="Event"/> based on given event ids</returns>
        public Task<IEnumerable<Event>> GetEventsAsync(IEnumerable<int> eventIds);
    }
}
