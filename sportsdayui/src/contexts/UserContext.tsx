// external imports
import { createContext, useReducer, useContext } from "react";

// internal imports
import { User, UserAction } from "../models/User";

const UserContext: React.Context<User | null> = createContext<User | null>(null);

const UserDispatchContext: React.Context<React.Dispatch<UserAction> | null> = createContext<React.Dispatch<UserAction> | null>(null);

function UserProvider({ children }: any) {
    const [user, dispatchUser] = useReducer(userReducer, null);
    console.log("here");
    return (
        <UserContext.Provider value={user}>
            <UserDispatchContext.Provider value={dispatchUser}>
                {children}
            </UserDispatchContext.Provider>
        </UserContext.Provider>
    )
}

function userReducer(_: User | null, action: UserAction): User | null {
    if (action?.type) {
        switch (action.type) {
            case "LOGGED_IN":
            case "SIGNED_IN":
                return action.user;
            case "LOGGED_OUT":
                return null;
        }
    }

    throw Error("Unknown action type or data:" + action?.type)
}

function useUser(): User | null {
    return useContext(UserContext);
}

function useUserDispatch(): React.Dispatch<UserAction> | null {
    return useContext(UserDispatchContext);
}

export {
    UserContext,
    UserDispatchContext,
    UserProvider,
    useUser,
    useUserDispatch
}