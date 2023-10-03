namespace sportsdayapi.Models
{
    using sportsdayapi.Models.DbModels;

    /// <summary>
    /// The response schema for get events request
    /// </summary>
    public class GetEventsResponse : BaseResponse
    {
        /// <summary>
        /// Gets or sets the list of events to be returned for the request
        /// </summary>
        public IEnumerable<Event> events { get; set; }
    }
}
