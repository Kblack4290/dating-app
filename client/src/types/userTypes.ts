export interface User {
    id: string;
    displayName: string;
    username: string;
    email: string;
    token: string;
    imageUrl?: string;
}

export interface LoginCreds {
    identifier: string;
    password: string;
}

export interface RegisterCreds {
    email: string;
    username: string;
    displayName: string;
    password: string;
}