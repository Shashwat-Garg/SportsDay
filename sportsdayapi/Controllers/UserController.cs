namespace sportsdayapi.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using sportsdayapi.Models;
    using sportsdayapi.Models.DbModels;
    using sportsdayapi.Services;
    using sportsdayapi.Transformers;

    /// <summary>
    /// Controller for user related requests
    /// </summary>
    [ApiController]
    [Route("api/users")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly IUserService _userService;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserController"/> class.
        /// </summary>
        /// <param name="logger">Logger for the class</param>
        /// <param name="userService">Service which handles user related methods</param>
        public UserController(ILogger<UserController> logger, IUserService userService)
        {
            this._logger = logger;
            this._userService = userService;
        }

        /// <summary>
        /// Create user with given inputs.
        /// In case user already exists or input is invalid, return appropriate error message.
        /// </summary>
        /// <param name="createUserData">Contains data needed for processing the request</param>
        /// <returns>An instance of <see cref="CreateUserResponse"/> containing an error message or success status.</returns>
        [HttpPost]
        [Route("create")]
        public async Task<ActionResult<CreateUserResponse>> CreateUser(CreateUserRequest createUserData)
        {
            try
            {
                if (createUserData == null || !createUserData.IsValid())
                {
                    // If request data is invalid, return invalid data error
                    return this.StatusCode(400, new CreateUserResponse { error_message = "INVALID_INPUT" });
                }

                // Add user if not already present
                User user = UserTransformer.CreateUserFromRequest(createUserData.user_id, createUserData.user_name);
                bool success = await this._userService.AddUserIfNotExistsAsync(user);
                if (success)
                {
                    return this.Ok(new CreateUserResponse { user = user });
                }
                else
                {
                    return this.StatusCode(400, new CreateUserResponse { error_message = "DUPLICATE_USER" });
                }
            }
            catch (Exception ex)
            {
                this._logger.LogError("CreateUser(): Exception: {0} | StackTrace: {1}", ex.Message, ex.StackTrace);
            }

            return this.StatusCode(500, new CreateUserResponse { error_message = "SERVER_ERROR" });
        }

        /// <summary>
        /// Authenticate a user with given inputs.
        /// In case user doesn't exist or input is invalid, return appropriate error message.
        /// </summary>
        /// <param name="loginRequest">Contains data needed for processing the request</param>
        /// <returns>An instance of <see cref="LoginUserResponse"/> containing an error message or requested user.</returns>
        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<LoginUserResponse>> LoginUser(LoginUserRequest loginRequest)
        {
            try
            {
                this._logger.LogInformation("User id: {0}", loginRequest?.user_id);
                if (loginRequest == null || !loginRequest.IsValid())
                {
                    // If request data is invalid, return invalid data error
                    return this.StatusCode(400, new LoginUserResponse { error_message = "INVALID_INPUT" });
                }

                User user = await this._userService.GetUserAsync(loginRequest.user_id);
                if (user == null)
                {
                    return this.StatusCode(400, new LoginUserResponse { error_message = "USER_ID_ABSENT" });
                }

                return new LoginUserResponse { user = user };
            }
            catch (Exception ex)
            {
                this._logger.LogError("LoginUser(): Exception: {0} | StackTrace: {1}", ex.Message, ex.StackTrace);
            }

            return this.StatusCode(500, new LoginUserResponse { error_message = "SERVER_ERROR" });
        }
    }
}