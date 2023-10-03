// external imports
import { useEffect } from "react";

// internal imports
import { GetEventsResponse } from "../../models/Events";
import { EventSchema } from "../../models/Event";
import Event from "../../components/Event/Event";
import { convertEventDbSchemaToEventSchema } from "../../helpers/transformationHelper";
import { areDatesEqual } from "../../helpers/dateHelper";
import { makeApiCallWithNumRetries } from "../../utils/networkCallHelper";
import { useUser } from "../../contexts/UserContext";
import { useEvents, useEventsDispatch } from "../../contexts/EventsContext";
import "./Events.css";

export default function Events() {
    const user = useUser();
    const events = useEvents();
    const dispatchEvents = useEventsDispatch();
    const userEvents = events.filter(event => event.registeredByUser);

    useEffect(() => {
        const getEvents = () => {
            makeApiCallWithNumRetries<undefined, GetEventsResponse>(
                "https://localhost:7266/api/events",
                "GET",
                undefined,
                3)
                .then((eventsResponse: GetEventsResponse | undefined) => {
                    if (eventsResponse?.events) {
                        // From DB, we get the list of events, whom we map to the event schema by setting default selected value as false
                        const allEvents: EventSchema[] = eventsResponse.events.map(event => { const newEvent: EventSchema = convertEventDbSchemaToEventSchema(event); return newEvent; });
                        if (dispatchEvents) {
                            dispatchEvents({
                                type: "SET_EVENTS",
                                events: allEvents,
                                event_id: undefined
                            });
                        }

                        // Once we've successfully recieved the list of events from BE, we'll ask the BE to tell us which of these belong to the current user
                        makeApiCallWithNumRetries<undefined, GetEventsResponse>(
                            "https://localhost:7266/api/events?user_id=" + user?.user_id,
                            "GET",
                            undefined,
                            3)
                            .then((userEventsResponse: GetEventsResponse | undefined) => {
                                if (userEventsResponse?.events) {
                                    const userEventIds: number[] = userEventsResponse.events.map(userEvent => userEvent.id);
                                    // From DB, we get the list of events, whom we map to the event schema by setting their registration value to true
                                    if (dispatchEvents) {
                                        userEventIds.forEach((eventId) => {
                                            dispatchEvents({
                                                type: "REGISTER_EVENT",
                                                events: undefined,
                                                event_id: eventId
                                            });
                                        });
                                    }
                                }
                            });
                    }
                });
        }

        getEvents();
    }, [user?.user_id, dispatchEvents]);

    function isCollidingWithRegisteredEvents(currentEvent: EventSchema): boolean {
        return userEvents
            .filter(event => {
                // events whose start time lies between start and end time of current event
                if (event.start_time > currentEvent.start_time && event.start_time < currentEvent.end_time) {
                    return true;
                }

                // events whose end time lies between start and end time of current event
                if (event.end_time > currentEvent.start_time && event.end_time < currentEvent.end_time) {
                    return true;
                }

                // events whose start and end time collide with current event
                if (areDatesEqual(event.end_time, currentEvent.end_time) && areDatesEqual(event.start_time, currentEvent.start_time)) {
                    return true;
                }

                return false;
            })
            .length > 0;
    }

    function isEventDisabled(currentEvent: EventSchema): boolean {
        // user can't register for more than 3 events
        if (userEvents.length >= 3) {
            return true;
        }

        // else if event collides with registered event
        return isCollidingWithRegisteredEvents(currentEvent);
    }

    return (
        <div className="events-page">
            <div className="events-page-item">
                <div className="events-page-item-title">All Events</div>
                <div className="events-page-item-cards">{
                    events
                        .filter(event => !event.registeredByUser)
                        .map(event => (
                            <Event
                                key={event.id}
                                event={event}
                                isDisabled={isEventDisabled(event)} />))}</div>
            </div>
            <div className="events-page-item">
                <div className="events-page-item-title">Selected Events</div>
                <div className="events-page-item-cards">{
                    events
                        .filter(event => event.registeredByUser)
                        .map(event => (
                            <Event
                                key={event.id}
                                event={event}
                                isDisabled={false} />))}</div>
            </div>
        </div>
    )
}