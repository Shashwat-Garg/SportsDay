import BaseResponse from "./BaseResponse";
import { User } from "./User";

type LoginUserRequest = {
    user_id: string;
}
type LoginUserResponse = BaseResponse & {
    user: User;
}

export {
    type LoginUserRequest,
    type LoginUserResponse
}