// internal imports
import { EventDbSchema, EventSchema } from "../models/Event";
import { RegisterEventRequest } from "../models/Events";
import { LoginUserRequest } from "../models/Login";
import { CreateUserRequest } from "../models/Signup";
import { convertStringToDate } from "./dateHelper";

export function buildCreateUserRequest(user_id: string, user_name?: string): CreateUserRequest {
    const reqObj: CreateUserRequest = {
        user_id,
        user_name
    };

    return reqObj;
}

export function buildLoginUserRequest(user_id: string): LoginUserRequest {
    const reqObj: LoginUserRequest = {
        user_id
    };

    return reqObj;
}

export function buildRegisterEventRequest(user_id: string, event_id: number) {
    const reqObj: RegisterEventRequest = {
        user_id,
        event_id
    };

    return reqObj;
}

export function convertEventDbSchemaToEventSchema(eventFromDb: EventDbSchema): EventSchema {
    const eventSchema: EventSchema = {
        id: eventFromDb.id,
        event_name: eventFromDb.event_name,
        event_category: eventFromDb.event_category,
        registeredByUser: false,
        start_time: convertStringToDate(eventFromDb.start_time),
        end_time: convertStringToDate(eventFromDb.end_time)
    };

    return eventSchema;
}