'use client';

import { getPagingQueryString } from '@/common/utils/string';
import { Order } from './order.types';
import HttpService from '@/common/services/http.service';
import { Hub } from '../hub/hub.types';

class OrderApiService extends HttpService {
  getOrders(options?: Partial<PagingOptions>) {
    const query = options ? getPagingQueryString(options) : '';
    return this.get<Paging<Order>>(`/orders/${query}`);
  }

  getOrderById(id: string) {
    return this.get<Order>(`/orders/${id}`);
  }

  getOrderHubPathById(id: string) {
    return this.get<Paging<Hub>>(`/orders/${id}/hubs/path`);
  }
}

const orderApi = new OrderApiService();

export default orderApi;
