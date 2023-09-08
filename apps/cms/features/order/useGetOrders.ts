'use client'
import { useQuery } from '@tanstack/react-query'
import orderApi from './order.service'
import { getPagingQueryString } from '@/common/utils/string'

const useGetOrders = (options?: Partial<PagingOptions>) => {
    const queryData = useQuery(
        [`orders/get${options ? `?${getPagingQueryString(options)}` : ''}`],
        {
            queryFn: () => orderApi.getOrders(options),
            // keepPreviousData: true,
        }
    )

    return queryData
}
export default useGetOrders
