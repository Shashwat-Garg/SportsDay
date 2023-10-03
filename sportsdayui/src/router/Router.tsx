// external imports
import { BrowserRouter, Routes, Route } from "react-router-dom";

// internal imports
import Login from "../components/Login/Login";
import Signup from "../components/Signup/Signup";
import Events from "../components/Events/Events";
import RestrictedRoute from "./RestrictedRoute";
export default function Router() {
    return (
        <BrowserRouter>
            <Routes>
                <Route path="/">
                    <Route index element={<Login />} />
                    <Route path="login" element={<Login />} />
                    <Route path="signup" element={<Signup />} />
                    <Route path="events" element={
                        <RestrictedRoute unauthorizedRoute="/login">
                            <Events />
                        </RestrictedRoute>
                        } />
                    <Route path="*" element={<Login />} />
                </Route>
            </Routes>
        </BrowserRouter>
    );
}