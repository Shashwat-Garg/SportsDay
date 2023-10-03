import BaseAction from "./BaseAction";
import BaseResponse from "./BaseResponse";
import { EventDbSchema, EventSchema } from "./Event";

type GetEventsResponse = BaseResponse & {
    events: EventDbSchema[];
}

type RegisterEventRequest = {
    user_id: string;
    event_id: number | undefined;
}

type RegisterEventResponse = BaseResponse & {
    success: boolean;
}

type EventsAction = BaseAction & {
    events: EventSchema[] | undefined;
    event_id: number | undefined;
}


export {
    type GetEventsResponse,
    type RegisterEventRequest,
    type RegisterEventResponse,
    type EventsAction
};