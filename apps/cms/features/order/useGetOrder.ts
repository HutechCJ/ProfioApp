'use client'
import { useQuery } from '@tanstack/react-query'
import orderApi from './order.service'

const useGetOrder = (id: string) => {
    const queryData = useQuery([`users/get/${id}`], {
        queryFn: () => orderApi.getOrderById(id),
        keepPreviousData: true,
    })

    return queryData
}
export default useGetOrder
