'use client'
import { useQuery } from '@tanstack/react-query'
import orderApi from './order.service'

const useGetOrderDeliveries = (id: string) => {
    const queryData = useQuery([`orders/get/${id}/deliveries`], {
        queryFn: () => orderApi.getOrderDeliveries(id),
        keepPreviousData: true,
    })

    return queryData
}
export default useGetOrderDeliveries
