import { useMutation } from '@tanstack/react-query'
import userApi from './user.service'
import { LoginRequest } from './user.types'

const useLogin = () => {
    return useMutation({
        mutationFn: (data: LoginRequest) => userApi.login(data),
    })
}

export default useLogin
