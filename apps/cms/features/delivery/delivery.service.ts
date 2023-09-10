'use client';

import HttpService from '@/common/services/http.service';
import { CreateDeliveryData, Delivery, UpdateDeliveryData } from './delivery.types';
import { getPagingQueryString } from '@/common/utils/string';

class DeliveryApiService extends HttpService {
    getDeliveries(options?: Partial<PagingOptions>) {
        const query = options ? getPagingQueryString(options) : ''
        return this.get<Paging<Delivery>>(`/deliveries/${query}`)
    }

    getDeliveryById(id: string) {
        return this.get<Delivery>(`/deliveries/${id}`)
    }

    createDelivery(data: CreateDeliveryData) {
        return this.post<Delivery>(`/deliveries`, data)
    }

    updateDelivery(id: string, data: UpdateDeliveryData) {
        return this.put(`/deliveries/${id}`, data)
    }

    deleteDelivery(id: string) {
        return this.delete<Delivery>(`/deliveries/${id}`)
    }
}

const deliveryApi = new DeliveryApiService();

export default deliveryApi;
