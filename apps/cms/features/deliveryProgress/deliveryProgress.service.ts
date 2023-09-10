'use client';

import HttpService from '@/common/services/http.service';
import {
  CreateDeliveryProgressData,
  DeliveryProgress,
  UpdateDeliveryProgressData,
} from './deliveryProgress.types';
import { getPagingQueryString } from '@/common/utils/string';

class DeliveryProgressApiService extends HttpService {
  getDeliveryProgresses(options?: Partial<PagingOptions>) {
    const query = options ? getPagingQueryString(options) : '';
    return this.get<Paging<DeliveryProgress>>(`/deliveryprogresses/${query}`);
  }

  getDeliveryProgressById(id: string) {
    return this.get<DeliveryProgress>(`/deliveryprogresses/${id}`);
  }

  createDeliveryProgress(data: CreateDeliveryProgressData) {
    return this.post<DeliveryProgress>(`/deliveryprogresses`, data);
  }

  updateDeliveryProgress(id: string, data: UpdateDeliveryProgressData) {
    return this.put(`/deliveryprogresses/${id}`, data);
  }

  deleteDeliveryProgress(id: string) {
    return this.delete<DeliveryProgress>(`/deliveryprogresses/${id}`);
  }
}

const deliveryProgressApi = new DeliveryProgressApiService();

export default deliveryProgressApi;
