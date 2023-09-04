export interface User {
    id: string
    userName: string
    email: string
    fullName: string
}

export type Password = {
    password: string
    confirmPassword: string
}

export type RegisterRequest = Omit<User, 'id'> & Password

export type LoginRequest = Pick<User, 'userName'> & Pick<Password, 'password'>

export interface AuthUserResponse extends User {
    token: string
    tokenExpire: Date
}
