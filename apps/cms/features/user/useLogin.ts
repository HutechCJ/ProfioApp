import { useMutation } from '@tanstack/react-query'
import userApi from './user.service'
import { LoginRequest } from './user.types'
import useLocalStorage from '@/common/hooks/useLocalStorage'
import StoreKeys from '@/common/constants/storekeys'

const useLogin = () => {
    const { set } = useLocalStorage()
    return useMutation({
        mutationFn: (data: LoginRequest) => userApi.login(data),
        onSuccess({ data }, variables, context) {
            console.log(data)
            set(StoreKeys.ACCESS_TOKEN, data.token)
        },
    })
}

export default useLogin
