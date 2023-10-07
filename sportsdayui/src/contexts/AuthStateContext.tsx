// external imports
import { createContext, useReducer, useContext } from "react";

// internal inports
import { AuthStateAction } from "../models/App";

const AuthStateContext: React.Context<boolean> = createContext<boolean>(false);

const AuthStateDispatchContext: React.Context<React.Dispatch<AuthStateAction> | null> = createContext<React.Dispatch<AuthStateAction> | null>(null);

function AuthStateProvider({ children }: any) {
    const [AuthState, dispatchAuthState] = useReducer(AuthStateReducer, false);

    return (
        <AuthStateContext.Provider value={AuthState} >
            <AuthStateDispatchContext.Provider value={dispatchAuthState}>
                {children}
            </AuthStateDispatchContext.Provider>
        </AuthStateContext.Provider>
    );
}

function AuthStateReducer(_: boolean, action: AuthStateAction): boolean {
    if (action?.type) {
        switch (action.type) {
            case "LOGGED_IN":
            case "SIGNED_IN":
                return true;
            case "LOGGED_OUT":
                return false;
        }
    }

    throw Error("Uknown action type or data:" + action?.type)
}

function useAuthState(): boolean {
    return useContext(AuthStateContext);
}

function useAuthStateDispatch(): React.Dispatch<AuthStateAction> | null {
    return useContext(AuthStateDispatchContext);
}

export {
    AuthStateContext,
    AuthStateDispatchContext,
    AuthStateProvider,
    useAuthState,
    useAuthStateDispatch
}