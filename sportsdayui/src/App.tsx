// external imports
import Router from "./router/Router";

// internal imports
import { AuthStateProvider } from "./contexts/AuthStateContext";
import { UserProvider } from "./contexts/UserContext";
import { EventsProvider } from "./contexts/EventsContext";
import "./styles/App.css";

function App() {
    return (
        <AuthStateProvider>
            <UserProvider>
                <EventsProvider>
                    <Router />
                </EventsProvider>
            </UserProvider>
        </AuthStateProvider>
    );
}

export default App;
