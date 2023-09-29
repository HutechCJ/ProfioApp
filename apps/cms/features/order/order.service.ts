'use client';

import { getPagingQueryString } from '@/common/utils/string';
import {
  CreateOrderData,
  EmailOrderData,
  Order,
  UpdateOrderData,
} from './order.types';
import HttpService from '@/common/services/http.service';
import { Hub } from '../hub/hub.types';
import { Delivery } from '../delivery/delivery.types';

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

  getOrderDeliveries(id: string) {
    return this.get<Paging<Delivery>>(`/orders/${id}/deliveries`);
  }

  countByStatus() {
    return this.get<number[]>(`/orders/count-by-status`);
  }

  createOrder(data: CreateOrderData) {
    return this.post<Order>(`/orders`, data);
  }

  updateOrder(data: UpdateOrderData) {
    return this.put(`/orders/${data.id}`, data);
  }

  deleteOrder(id: string) {
    return this.delete<Order>(`/orders/${id}`);
  }

  senderEmailOrder(data: EmailOrderData) {
    return this.post(`/sender/email/order`, data);
  }
}

const orderApi = new OrderApiService();

export default orderApi;
