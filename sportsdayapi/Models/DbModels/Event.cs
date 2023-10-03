namespace sportsdayapi.Models.DbModels
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Schema for the Event table in DB
    /// </summary>
    public class Event
    {
        /// <summary>
        /// Gets or sets the event id, which is the primary key for a record
        /// </summary>
        [Key]
        public int id { get; set; }

        /// <summary>
        /// Gets or sets the event name
        /// </summary>
        public string event_name { get; set; }

        /// <summary>
        /// Gets or sets the event category
        /// </summary>
        public string event_category { get; set; }

        /// <summary>
        /// Gets or sets the start time of an event
        /// </summary>
        public DateTime start_time { get; set; }

        /// <summary>
        /// Gets or sets the end time of an event
        /// </summary>
        public DateTime end_time { get; set; }

        /// <summary>
        /// Gets or sets the list of users who have registered to this event
        /// This is a navigation property, meaning only when it is needed, it will be instantiated using the foreign key constraints
        /// </summary>
        public ICollection<UserEvent> user_events { get; set; }
    }
}
