'use client';

import HttpService from '@/common/services/http.service';
import {
  CreateOrderHistoryData,
  OrderHistory,
  UpdateOrderHistoryData,
} from './orderHistory.types';
import { getPagingQueryString } from '@/common/utils/string';

class OrderHistoryApiService extends HttpService {
  getOrderHistories(options?: Partial<PagingOptions>) {
    const query = options ? getPagingQueryString(options) : '';
    return this.get<Paging<OrderHistory>>(`/orderhistories/${query}`);
  }

  getOrderHistoryById(id: string) {
    return this.get<OrderHistory>(`/orderhistories/${id}`);
  }

  createOrderHistory(data: CreateOrderHistoryData) {
    return this.post<OrderHistory>(`/orderhistories`, data);
  }

  updateOrderHistory(id: string, data: UpdateOrderHistoryData) {
    return this.put(`/orderhistories/${id}`, data);
  }

  deleteOrderHistory(id: string) {
    return this.delete<OrderHistory>(`/orderhistories/${id}`);
  }
}

const orderHistoryApi = new OrderHistoryApiService();

export default orderHistoryApi;
