import BaseResponse from "./BaseResponse";
import { User } from "./User";

type CreateUserRequest = {
    user_id: string;
    user_name?: string;
}

type CreateUserResponse = BaseResponse & {
    user: User;
}

export {
    type CreateUserRequest,
    type CreateUserResponse
}