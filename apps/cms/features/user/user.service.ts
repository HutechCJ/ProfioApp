'use client';

import HttpService from '@/common/services/http.service';
import {
  AuthUser,
  AuthUserResponse,
  ChangePassword,
  LoginRequest,
  RegisterRequest,
  User,
} from './user.types';
import { getPagingQueryString } from '@/common/utils/string';

class UserApiService extends HttpService {
  register(data: RegisterRequest) {
    return this.post<AuthUserResponse>('/users/register', data);
  }

  login(data: LoginRequest) {
    return this.post<AuthUser>('/users/login', data);
  }

  getUserById(id: string) {
    return this.get<User>(`/users/${id}`);
  }

  getUsers(options?: Partial<PagingOptions>) {
    const query = options ? getPagingQueryString(options) : '';
    return this.get<Paging<User>>(`/users/get-users/${query}`);
  }

  checkAuthorization() {
    return this.get<User>('/users/check-authorization');
  }

  changePassword(data: ChangePassword) {
    return this.post<AuthUser>('/users/change-password', data);
  }
}

const userApi = new UserApiService();

export default userApi;
