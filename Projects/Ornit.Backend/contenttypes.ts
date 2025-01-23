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

