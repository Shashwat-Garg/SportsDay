import BaseAction from "./BaseAction";

type User = {
    user_id: string;
    user_name?: string;
}

type UserAction = BaseAction & {
    user: User | null
}

export {
    type User,
    type UserAction
}