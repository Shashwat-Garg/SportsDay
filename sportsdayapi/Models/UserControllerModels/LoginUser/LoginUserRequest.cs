namespace sportsdayapi.Models
{
    /// <summary>
    /// The schema for the request regarding user login
    /// Can add other fields like password as and when needed
    /// </summary>
    public class LoginUserRequest
    {
        /// <summary>
        /// Gets or sets the user id received as part of the request
        /// </summary>
        public string user_id { get; set; }

        /// <summary>
        /// Checks whether the object instance is a valid user login request
        /// </summary>
        /// <returns>The validity of the request object</returns>
        public bool IsValid()
        {
            return !string.IsNullOrWhiteSpace(this.user_id);
        }
    }
}
