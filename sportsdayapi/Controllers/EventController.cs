namespace sportsdayapi.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using sportsdayapi.Models;
    using sportsdayapi.Models.DbModels;
    using sportsdayapi.Services;
    using sportsdayapi.Transformers;

    /// <summary>
    /// Controller for event related requests
    /// </summary>
    [ApiController]
    [Route("api/events")]
    public class EventController : ControllerBase
    {
        private readonly ILogger<EventController> _logger;
        private readonly IEventService _eventService;
        private readonly IUserEventService _userEventService;

        /// <summary>
        /// Initializes a new instance of the <see cref="EventController"/> class.
        /// </summary>
        /// <param name="logger">Logger for the class</param>
        /// <param name="eventService">Service which handles event related methods</param>
        /// <param name="userEventService">Service which handles UserEvent related methods</param>
        public EventController(ILogger<EventController> logger, IEventService eventService, IUserEventService userEventService)
        {
            this._logger = logger;
            this._eventService = eventService;
            this._userEventService = userEventService;
        }

        /// <summary>
        /// Returns list of events or error message regarding the request.
        /// The list of events can either be all the events or events specifically for the user depending on the request input.
        /// </summary>
        /// <param name="user_id">Present if events are needed for this specific user id</param>
        /// <returns>An instance of <see cref="GetEventsResponse"/> containing an error message or events requested</returns>
        [HttpGet]
        public async Task<ActionResult<GetEventsResponse>> GetEvents(string user_id = null)
        {
            try
            {
                // We want all events here
                if (string.IsNullOrWhiteSpace(user_id))
                {
                    IEnumerable<Event> events = await this._eventService.GetEventsAsync();
                    return this.Ok(new GetEventsResponse { events = events });
                }

                IEnumerable<Event> userEvents = await this._userEventService.GetEventsForUserAsync(user_id);
                return this.Ok(new GetEventsResponse { events = userEvents });
            }
            catch (Exception ex)
            {
                this._logger.LogError("GetEvents(): Exception: {0} | StackTrace: {1}", ex.Message, ex.StackTrace);
            }

            return this.StatusCode(500, new GetEventsResponse { error_message = "SERVER_ERROR" });
        }

        /// <summary>
        /// Registers a user for the given event.
        /// Also returns an error message in case there is one.
        /// </summary>
        /// <param name="registerEventRequest">Contains data needed for processing the request</param>
        /// <returns>An instance of <see cref="RegisterEventResponse"/> containing an error message and success status.</returns>
        [HttpPut]
        [Route("registration")]
        public async Task<ActionResult<RegisterEventResponse>> RegisterUserForEvent(RegisterEventRequest registerEventRequest)
        {
            try
            {
                if (registerEventRequest == null || !registerEventRequest.IsValid())
                {
                    return this.StatusCode(400, new RegisterEventResponse { error_message = "INVALID_INPUT" });
                }

                UserEvent userEvent = UserEventTransformer.CreateUserEventFromRequest(registerEventRequest.user_id, registerEventRequest.event_id);
                bool success = await this._userEventService.RegisterUserForEventIfNotRegisteredAsync(userEvent);
                if (!success)
                {
                    return this.StatusCode(400, new RegisterEventResponse { error_message = "INVALID_DATA" });
                }

                return this.Ok(new RegisterEventResponse { success = true });
            }
            catch (Exception ex)
            {
                this._logger.LogError("RegisterUserForEvent(): Exception: {0} | StackTrace: {1}", ex.Message, ex.StackTrace);
            }

            return this.StatusCode(500, new RegisterEventResponse { error_message = "SERVER_ERROR" });
        }

        /// <summary>
        /// Unregisters a user from the given event.
        /// Also returns an error message in case there is one.
        /// </summary>
        /// <param name="registerEventRequest">Contains data needed for processing the request</param>
        /// <returns>An instance of <see cref="RegisterEventResponse"/> containing an error message and success status.</returns>
        [HttpDelete]
        [Route("registration")]
        public async Task<ActionResult<RegisterEventResponse>> UnRegisterUserForEvent(RegisterEventRequest registerEventRequest)
        {
            try
            {
                if (registerEventRequest == null || !registerEventRequest.IsValid())
                {
                    return this.StatusCode(400, new RegisterEventResponse { error_message = "INVALID_INPUT" });
                }

                UserEvent userEvent = UserEventTransformer.CreateUserEventFromRequest(registerEventRequest.user_id, registerEventRequest.event_id);
                bool success = await this._userEventService.UnRegisterUserForEventAsync(userEvent);
                if (!success)
                {
                    return this.StatusCode(400, new RegisterEventResponse { error_message = "INVALID_DATA" });
                }

                return this.Ok(new RegisterEventResponse { success = success });
            }
            catch (Exception ex)
            {
                this._logger.LogError("DeRegisterUserForEvent(): Exception: {0} | StackTrace: {1}", ex.Message, ex.StackTrace);
            }

            return this.StatusCode(500, new RegisterEventResponse { error_message = "SERVER_ERROR" });
        }
    }
}
