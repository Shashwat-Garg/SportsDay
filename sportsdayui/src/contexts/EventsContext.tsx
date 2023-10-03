// external impports
import { createContext, useReducer, useContext } from "react";

// internal imports
import { EventSchema } from "../models/Event";
import { EventsAction } from "../models/Events";

const EventsContext: React.Context<EventSchema[]> = createContext<EventSchema[]>([]);

const EventsDispatchContext: React.Context<React.Dispatch<EventsAction> | null> = createContext<React.Dispatch<EventsAction> | null>(null);

function EventsProvider({ children }: any) {
    const [events, dispatchEvents] = useReducer(eventsReducer, []);
    return (
        <EventsContext.Provider value={events}>
            <EventsDispatchContext.Provider value={dispatchEvents}>
                {children}
            </EventsDispatchContext.Provider>
        </EventsContext.Provider>
    )
}

function eventsReducer(events: EventSchema[], action: EventsAction): EventSchema[] {
    if (action?.type) {
        switch (action.type) {
            case "SET_EVENTS":
                if (action.events) {
                    return action.events;
                }

                break;
            case "REGISTER_EVENT":
                if (action.event_id) {
                    return events.map(event => {
                        if (event.id === action.event_id) {
                            event.registeredByUser = true;
                        }

                        return event;
                    });
                }

                break;
            case "UNREGISTER_EVENT":
                if (action.event_id) {
                    return events.map(event => {
                        if (event.id === action.event_id) {
                            event.registeredByUser = false;
                        }

                        return event;
                    });
                }

                break;
        }
    }

    throw Error("Unknown action type or data:" + action?.type)
}

function useEvents(): EventSchema[] {
    return useContext(EventsContext);
}

function useEventsDispatch(): React.Dispatch<EventsAction> | null {
    return useContext(EventsDispatchContext);
}

export {
    EventsContext,
    EventsDispatchContext,
    EventsProvider,
    useEvents,
    useEventsDispatch
}