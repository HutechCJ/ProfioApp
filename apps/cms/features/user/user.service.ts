'use client'
import {
    RegisterRequest,
    AuthUserResponse,
    User,
    LoginRequest,
} from './user.types'
import HttpService from '@/common/services/http.service'

class UserApiService extends HttpService {
    constructor() {
        super({ baseURL: 'https://64f593622b07270f705d628f.mockapi.io/api/' })
    }

    register(data: RegisterRequest) {
        return this.post<AuthUserResponse>('/users', data)
    }

    login(data: LoginRequest) {
        return this.post<AuthUserResponse>('/users/login', data)
    }

    getUserById(id: string) {
        return this.get<User>(`/users/${id}`)
    }

    getUsers() {
        return this.get<User[]>(`/users`)
    }
}

const userApi = new UserApiService()

export default userApi
