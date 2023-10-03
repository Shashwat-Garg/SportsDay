type EventDbSchema = {
    id: number;
    event_name: string;
    event_category: string;
    start_time: string;
    end_time: string;
}

type EventSchema = {
    id: number;
    event_name: string;
    event_category: string;
    start_time: Date;
    end_time: Date;
    registeredByUser: boolean;
}

type EventProps = {
    event: EventSchema;
    isDisabled: boolean;
}

export {
    type EventDbSchema,
    type EventSchema,
    type EventProps
};