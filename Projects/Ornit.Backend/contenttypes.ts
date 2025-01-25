export interface UserDto {
    id: number;
    auth0Id: string;
    email: string;
}

export interface UserEntity {
    id: number;
    auth0Id: string;
    email: string;
    users: UserDto[];
    ints: number[];
    userMap: Map<number, UserDto>;
    iUserMap: Map<number, UserDto>;
    hash: Set<UserDto>;
}

export interface Auth0LoginResponse {
    accessToken: string;
    refreshToken: string;
    scope: string;
    expiresIn: number;
    tokenType: string;
}

export interface Auth0RegisterResponse {
    id: string;
    email: string;
    emailVerified: boolean;
}

export interface AuthRequest {
    email: string;
    password: string;
}

