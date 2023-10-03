// external imports
import { Button } from "@mui/material";

// internal imports
import { EventProps } from "../../models/Event";
import { RegisterEventRequest, RegisterEventResponse } from "../../models/Events";
import { buildRegisterEventRequest } from "../../helpers/transformationHelper";
import { getFormattedTime } from "../../helpers/dateHelper";
import { useUser } from "../../contexts/UserContext";
import { useEventsDispatch } from "../../contexts/EventsContext";
import { makeApiCallWithNumRetries } from "../../utils/networkCallHelper";
import "./Event.css";

export default function Event(props: EventProps) {
    const user = useUser();
    const dispatchEvents = useEventsDispatch();

    function updateRegistration() {
        const registrationRequest: RegisterEventRequest = buildRegisterEventRequest(user!.user_id, props.event.id);
        if (!props.event.registeredByUser) {
            makeApiCallWithNumRetries<RegisterEventRequest, RegisterEventResponse>(
                "https://localhost:7266/api/events/registration",
                "PUT",
                registrationRequest,
                3)
                .then((registerResponse: RegisterEventResponse | undefined) => {
                    if (registerResponse?.success && dispatchEvents) {
                        dispatchEvents({
                            type: "REGISTER_EVENT",
                            events: undefined,
                            event_id: props.event.id
                        });
                    }
                });
        }
        else {
            makeApiCallWithNumRetries<RegisterEventRequest, RegisterEventResponse>(
                "https://localhost:7266/api/events/registration",
                "DELETE",
                registrationRequest,
                3)
                .then((unregisterResponse: RegisterEventResponse | undefined) => {
                    if (unregisterResponse?.success && dispatchEvents) {
                        dispatchEvents({
                            type: "UNREGISTER_EVENT",
                            events: undefined,
                            event_id: props.event.id
                        });
                    }
                });
        }
    }

    return (
        <div className={"event" + (props.isDisabled ? " disabled" : "")}>
            <div className="event-category-letter">{props.event.event_category[0]}</div>
            <div className="event-name">{props.event.event_name}</div>
            <div className="event-category">({props.event.event_category})</div>
            <div className="event-time">{getFormattedTime(props.event.start_time)} - {getFormattedTime(props.event.end_time)}</div>
            <Button
                className={"event-register-button" + (props.event.registeredByUser ? " selected" : "") + (props.isDisabled ? " disabled" : "")}
                variant="contained"
                disabled={props.isDisabled}
                onClick={() => updateRegistration()}>{props.event.registeredByUser ? "REMOVE" : "SELECT"}</Button>
        </div>
    )
}