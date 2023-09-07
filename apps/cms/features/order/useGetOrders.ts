'use client'
import { useQuery } from '@tanstack/react-query'
import orderApi from './order.service'

const useGetOrders = (options?: Partial<PagingOptions>) => {
    const queryData = useQuery([`orders/get`], {
        queryFn: () => orderApi.getOrders(options),
    })

    return queryData
}
export default useGetOrders
