// external imports
import { useState } from "react";
import { Alert, Button, TextField } from "@mui/material";
import { useNavigate } from "react-router-dom";

// internal imports
import { buildLoginUserRequest } from "../../helpers/transformationHelper";
import { getErrorStringFromCode } from "../../utils/errorUtil";
import { LoginUserRequest, LoginUserResponse } from "../../models/Login";
import { makeApiCallWithNumRetries } from "../../utils/networkCallHelper";
import { useUserDispatch } from "../../contexts/UserContext";
import { useAuthStateDispatch } from "../../contexts/AuthStateContext";

export default function Login() {
    const [userId, setUserId] = useState("");
    const [error, setError] = useState("");
    const navigate = useNavigate();
    const dispatchUser = useUserDispatch();
    const dispatchAuthState = useAuthStateDispatch();

    function onSubmit() {
        const loginRequest: LoginUserRequest = buildLoginUserRequest(userId);
        makeApiCallWithNumRetries<LoginUserRequest, LoginUserResponse>(
            "https://localhost:7266/api/users/login",
            "POST",
            loginRequest,
            3)
            .then((loginResponse: LoginUserResponse | undefined) => {
                if (loginResponse?.user) {
                    if (dispatchUser) {
                        dispatchUser({
                            type: "LOGGED_IN",
                            user: loginResponse.user
                        });
                    }

                    if (dispatchAuthState) {
                        dispatchAuthState({
                            type: "LOGGED_IN"
                        });
                    }

                    navigate("/events");
                }
                else {
                    setError(getErrorStringFromCode(loginResponse?.error_message));
                }
            });
    }

    return (
        <div className="form">
            <div className="form-item form-title">Login</div>
            {error && (<Alert severity="error">Error: {error}</Alert>)}
            <TextField
                className="form-item form-input-text"
                variant="standard"
                placeholder="User id"
                value={userId}
                onChange={e => setUserId(e.target.value)} />

            <Button
                className="form-item form-input-button"
                variant="contained"
                onClick={() => { onSubmit(); }}>Submit</Button>

            <Button
                className="form-item form-input-button"
                variant="contained"
                onClick={() => { navigate("/signup"); }}>Signup</Button>
        </div>
    )
}