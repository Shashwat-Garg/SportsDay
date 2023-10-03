namespace sportsdayapi.Transformers
{
    using sportsdayapi.Models.DbModels;

    /// <summary>
    /// Contains helper methods regarding <see cref="UserEvent"/> model.
    /// </summary>
    public static class UserEventTransformer
    {
        /// <summary>
        /// Creates <see cref="UserEvent"/> from request.
        /// </summary>
        /// <param name="userId">user id</param>
        /// <param name="eventId"> event id</param>
        /// <returns>The transformed <see cref="UserEvent"/></returns>
        public static UserEvent CreateUserEventFromRequest(string userId, int eventId)
        {
            return new UserEvent { user_id = userId, event_id = eventId };
        }
    }
}
