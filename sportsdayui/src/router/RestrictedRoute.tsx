// external imports
import { useEffect } from "react";
import { useNavigate } from "react-router-dom";

// internal imports
import { useAuthState } from "../contexts/AuthStateContext";
import { RestrictedRouteProps } from "../models/Router";

export default function RestrictedRoute(props: RestrictedRouteProps) {
    const navigate = useNavigate();
    const isUserAuthorized: boolean = useAuthState();
    useEffect(() => {
        if (!isUserAuthorized) {
            navigate(props.unauthorizedRoute);
        }
    }, [navigate, isUserAuthorized, props.unauthorizedRoute]);

    if (isUserAuthorized) {
        return (
            props.children
        );
    }
}