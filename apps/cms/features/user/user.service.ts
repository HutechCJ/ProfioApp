'use client'

import {
    RegisterRequest,
    AuthUserResponse,
    User,
    LoginRequest,
    AuthCheckResponse,
    AuthUser,
} from './user.types'
import HttpService from '@/common/services/http.service'

class UserApiService extends HttpService {
    constructor() {
        super({
            baseURL: '/api/users',
            withCredentials: true,
        })
    }

    register(data: RegisterRequest) {
        return this.post<AuthUserResponse>('/register', data)
    }

    login(data: LoginRequest) {
        return this.post<AuthUser>('/login', data)
    }

    getUserById(id: string) {
        return this.get<User>(`/${id}`)
    }

    getUsers() {
        return this.get<User[]>(`/get-users`)
    }

    checkAuthorization() {
        return this.get<User>(`/check-authorization`)
    }
}

const userApi = new UserApiService()

export default userApi
