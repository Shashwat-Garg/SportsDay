namespace sportsdayapi.Models.DbModels
{
    /// <summary>
    /// Schema for the UserEvent table in DB
    /// This table stores which user has registered for which event using their ids as keys
    /// </summary>
    public class UserEvent
    {
        /// <summary>
        /// Gets or sets the user id for the record.
        /// This is a part of the combined primary key.
        /// </summary>
        public string user_id { get; set; }

        /// <summary>
        /// Gets or sets the user for the record.
        /// This is a navigation property, meaning only when it is needed, it will be instantiated using the foreign key constraints.
        /// </summary>
        public User user { get; set; }

        /// <summary>
        /// Gets or sets the event id for the record.
        /// This is a part of the combined primary key.
        /// </summary>
        public int event_id { get; set; }

        /// <summary>
        /// Gets or sets the event for the record.
        /// This is a navigation property, meaning only when it is needed, it will be instantiated using the foreign key constraints.
        /// </summary>
        public Event @event { get; set; }
    }
}