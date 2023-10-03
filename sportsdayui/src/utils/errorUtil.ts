function getErrorStringFromCode(errorCode?: string): string {
    if (errorCode) {
        switch (errorCode) {
            case "INVALID_INPUT":
                return "An input was invalid, please ensure all fields are duly filled!";
            case "USER_ID_ABSENT":
                return "The user does not exist, please enter a valid user id";
            case "DUPLICATE_USER":
                return "User id already exists, please use another user id";
        }
    }

    return "An unknown error occurred, please try refreshing the page";
}

export {
    getErrorStringFromCode
};