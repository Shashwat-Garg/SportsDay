namespace sportsdayapi.Services
{
    using Microsoft.EntityFrameworkCore;
    using sportsdayapi.Data;
    using sportsdayapi.Models.DbModels;

    /// <inheritdoc/>
    public class EventService : IEventService
    {
        private readonly DataContext _dbContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="EventService"/> class.
        /// </summary>
        /// <param name="context">The database context</param>
        public EventService(DataContext context)
        {
            this._dbContext = context;
        }

        /// <inheritdoc/>
        public async Task<bool> DoesEventExistAsync(int eventId)
        {
            Event @event = await this.GetEventAsync(eventId);
            return @event != null;
        }

        /// <inheritdoc/>
        public async Task<Event> GetEventAsync(int eventId)
        {
            return await this._dbContext.Events.FirstOrDefaultAsync(@event => @event.id == eventId);
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<Event>> GetEventsAsync()
        {
            return await this._dbContext.Events.ToListAsync();
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<Event>> GetEventsAsync(IEnumerable<int> eventIds)
        {
            return await this._dbContext.Events
                .Where(@event => eventIds.Contains(@event.id))
                .ToListAsync();
        }
    }
}
