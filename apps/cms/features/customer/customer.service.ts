'use client';

import HttpService from '@/common/services/http.service';
import {
  CreateCustomerData,
  Customer,
  UpdateCustomerData,
} from './customer.types';
import { getPagingQueryString } from '@/common/utils/string';

class CustomerApiService extends HttpService {
  getCustomers(options?: Partial<PagingOptions>) {
    const query = options ? getPagingQueryString(options) : '';
    return this.get<Paging<Customer>>(`/customers/${query}`);
  }

  getCustomerById(id: string) {
    return this.get<Customer>(`/customers/${id}`);
  }

  createCustomer(data: CreateCustomerData) {
    return this.post<Customer>(`/customers`, data);
  }

  updateCustomer(id: string, data: UpdateCustomerData) {
    return this.put(`/customers/${id}`, data);
  }

  deleteCustomer(id: string) {
    return this.delete<Customer>(`/customers/${id}`);
  }
}

const customerApi = new CustomerApiService();

export default customerApi;
