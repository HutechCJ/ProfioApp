import { Delivery } from '../delivery/delivery.types'
import { Hub } from '../hub/hub.types'

export type OrderHistory = {
    id: string
    timestamp: string
    delivery: Delivery
    hub: Hub
}

export type CreateOrderHistoryData = Omit<
    OrderHistory,
    'id' | 'delivery' | 'hub'
> & {
    hubId: string
    deliveryId: string
}

export type UpdateOrderHistoryData = Omit<OrderHistory, 'delivery' | 'hub'> & {
    hubId: string
    deliveryId: string
}
