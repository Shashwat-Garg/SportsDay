namespace sportsdayapi.Models
{
    using sportsdayapi.Models.DbModels;

    /// <summary>
    /// The schema for the response to be sent for a user creation request
    /// </summary>
    public class CreateUserResponse : BaseResponse
    {
        /// <summary>
        /// Gets or sets the final user data for the request
        /// </summary>
        public User user { get; set; } = null;
    }
}
