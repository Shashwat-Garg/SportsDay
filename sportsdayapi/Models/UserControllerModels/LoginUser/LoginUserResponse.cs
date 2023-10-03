namespace sportsdayapi.Models
{
    using sportsdayapi.Models.DbModels;

    /// <summary>
    /// The schema for the response to be sent for a user login request
    /// </summary>
    public class LoginUserResponse : BaseResponse
    {
        /// <summary>
        /// Gets or sets the user data to be present in the response
        /// </summary>
        public User user { get; set; } = null;
    }
}
