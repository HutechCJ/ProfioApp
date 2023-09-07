'use client'
import { useQuery } from '@tanstack/react-query'
import userApi from './user.service'

const useUser = () => {
    const { data } = useQuery([`checkAuthorization`], {
        queryFn: () => userApi.checkAuthorization(),
        keepPreviousData: true,
    })

    return data ?? null
}
export default useUser
