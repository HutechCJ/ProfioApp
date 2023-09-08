'use client'

import { Order } from './order.types'
import HttpService from '@/common/services/http.service'

class OrderApiService extends HttpService {
    getOrders(options?: Partial<PagingOptions>) {
        let query = ''
        if (options) {
            query += '?'
            query += Object.entries(options)
                .map((value) => `${value[0]}=${value[1]}`)
                .join('&')
        }
        return this.get<Paging<Order>>(`/orders/${query}`)
    }

    getOrderById(id: string) {
        return this.get<Order>(`/orders/${id}`)
    }
}

const orderApi = new OrderApiService()

export default orderApi
