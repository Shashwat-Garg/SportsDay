namespace sportsdayapi.Models
{
    /// <summary>
    /// The base schema for any response from API
    /// </summary>
    public class BaseResponse
    {
        /// <summary>
        /// Gets or sets the error message if any
        /// </summary>
        public string error_message { get; set; } = null;
    }
}
