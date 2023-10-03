namespace sportsdayapi.Models.DbModels
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Schema for the User table in DB
    /// </summary>
    public class User
    {
        /// <summary>
        /// Gets or sets the user id, which is the primary key for a record
        /// </summary>
        [Key]
        public string user_id { get; set; }

        /// <summary>
        /// Gets or sets the user name, it is allowed to be empty
        /// </summary>
        public string user_name { get; set; }

        /// <summary>
        /// Gets or sets the list of events the user has registered to.
        /// This is a navigation property, meaning only when it is needed, it will be instantiated using the foreign key constraints.
        /// </summary>
        public ICollection<UserEvent> user_events { get; set; }
    }
}
