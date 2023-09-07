'use client'

import { Order } from './order.types'
import HttpService from '@/common/services/http.service'

class OrderApiService extends HttpService {
    constructor() {
        super({
            baseURL: '/api/orders',
            withCredentials: true,
        })
    }

    getOrders(options?: Partial<PagingOptions>) {
        let query = ''
        if (options) {
            query += '?'
            query += Object.entries(options)
                .map((value) => `${value[0]}=${value[1]}`)
                .join('&')
        }
        return this.get<ApiResponse<Paging<Order>>>(`/${query}`)
    }

    getOrderById(id: string) {
        return this.get<Order>(`/${id}`)
    }
}

const orderApi = new OrderApiService()

export default orderApi
