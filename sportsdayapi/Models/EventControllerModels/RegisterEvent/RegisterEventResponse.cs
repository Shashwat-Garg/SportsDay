namespace sportsdayapi.Models
{
    /// <summary>
    /// The response schema for requests regarding user registration for an event
    /// </summary>
    public class RegisterEventResponse : BaseResponse
    {
        /// <summary>
        /// Gets or sets a value indicating whether request was successfully completed.
        /// </summary>
        public bool success { get; set; } = false;
    }
}
