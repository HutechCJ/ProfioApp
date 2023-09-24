export type User = {
  id: string;
  userName: string;
  email: string;
  fullName: string;
};

export type Password = {
  password: string;
  confirmPassword: string;
};

export type ChangePassword = {
  oldPassword: string;
  newPassword: string;
  confirmPassword: string;
};

export type RegisterRequest = Omit<User, 'id'> & Password;

export type LoginRequest = Pick<User, 'userName'> & Pick<Password, 'password'>;

export type AuthCheckResponse = ApiResponse<User>;

export type Tokens = { token: string; tokenExpire: Date };

export type AuthUser = User & Tokens;

export type AuthUserResponse = ApiResponse<AuthUser>;
