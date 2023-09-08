'use client'

import HttpService from '@/common/services/http.service'
import {
    AuthUser,
    AuthUserResponse,
    LoginRequest,
    RegisterRequest,
    User
} from './user.types'

class UserApiService extends HttpService {
    // constructor() {
    //     super({
    //         withCredentials: true,
    //     })
    // }

    register(data: RegisterRequest) {
        return this.post<AuthUserResponse>('/users/register', data)
    }

    login(data: LoginRequest) {
        return this.post<AuthUser>('/users/login', data)
    }

    getUserById(id: string) {
        return this.get<User>(`/users/${id}`)
    }

    getUsers() {
        return this.get<Paging<User>>(`/users/get-users`)
    }

    checkAuthorization() {
        return this.get<User>(`/users/check-authorization`)
    }
}

const userApi = new UserApiService()

export default userApi
