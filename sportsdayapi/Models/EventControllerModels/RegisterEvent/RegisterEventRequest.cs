namespace sportsdayapi.Models
{
    /// <summary>
    /// The schema for the request regarding user registration
    /// </summary>
    public class RegisterEventRequest
    {
        /// <summary>
        /// Gets or sets the user id received as part of the request
        /// </summary>
        public string user_id { get; set; }

        /// <summary>
        /// Gets or sets the event id received as part of the request
        /// </summary>
        public int event_id { get; set; }

        /// <summary>
        /// Checks whether the request is valid
        /// </summary>
        /// <returns>The validity of the request object</returns>
        public bool IsValid()
        {
            return !string.IsNullOrWhiteSpace(this.user_id) && this.event_id > 0;
        }
    }
}
