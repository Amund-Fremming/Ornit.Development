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

export interface TestClass {
    prop1: string;
    prop2: string;
}

export enum TestEnum {
    One,
    Two,
    Three,
}
export interface TestRecord {
    testNOOOOWO: string;
    recordString: string;
}

export interface TestStruct {
    p4: string;
    p1: string;
    p2: string;
    p3: string;
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

