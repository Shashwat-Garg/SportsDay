// external imports
import { useState } from "react";
import { Alert, Button, TextField } from "@mui/material";
import { useNavigate } from "react-router-dom";

// internal imports
import { buildCreateUserRequest } from "../../helpers/transformationHelper";
import { getErrorStringFromCode } from "../../utils/errorUtil";
import { CreateUserRequest, CreateUserResponse } from "../../models/Signup";
import { makeApiCallWithNumRetries } from "../../utils/networkCallHelper";
import { useUserDispatch } from "../../contexts/UserContext";
import { useAuthStateDispatch } from "../../contexts/AuthStateContext";

export default function Signup() {
    const [userId, setUserId] = useState("");
    const [userName, setUserName] = useState("");
    const [error, setError] = useState("");
    const dispatchUser = useUserDispatch();
    const dispatchAuthState = useAuthStateDispatch();
    const navigate = useNavigate();

    function onSubmit() {
        const signupRequest: CreateUserRequest = buildCreateUserRequest(userId, userName);
        makeApiCallWithNumRetries<CreateUserRequest, CreateUserResponse>(
            "https://localhost:7266/api/users/create",
            "POST",
            signupRequest,
            3)
            .then((signupResponse: CreateUserResponse | undefined) => {
                if (signupResponse?.user) {
                    if (dispatchUser) {
                        dispatchUser({
                            type: "SIGNED_IN",
                            user: signupResponse.user
                        });
                    }

                    if (dispatchAuthState) {
                        dispatchAuthState({
                            type: "SIGNED_IN"
                        });
                    }

                    navigate("/events");
                }
                else {
                    setError(getErrorStringFromCode(signupResponse?.error_message));
                }
            });
    }

    return (
        <div className="form">
            <div className="form-title">Signup</div>
            {error && (<Alert severity="error">Error: {error}</Alert>)}
            <TextField
                className="form-item form-input-text"
                variant="standard"
                placeholder="User id"
                value={userId}
                onChange={e => setUserId(e.target.value)} />

            <TextField
                className="form-item form-input-text"
                variant="standard"
                placeholder="User name"
                value={userName}
                onChange={e => setUserName(e.target.value)} />

            <Button
                className="form-item form-input-button"
                variant="contained"
                onClick={() => { onSubmit(); }}>Submit</Button>

            <Button
                className="form-item form-input-button"
                variant="contained"
                onClick={() => { navigate("/login") }}>Login</Button>
        </div>
    )
}