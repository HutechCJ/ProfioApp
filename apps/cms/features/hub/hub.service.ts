'use client';

import HttpService from '@/common/services/http.service';
import { CreateHubData, Hub, UpdateHubData } from './hub.types';
import { getPagingQueryString } from '@/common/utils/string';

class HubApiService extends HttpService {
  getHubs(options?: Partial<PagingOptions>) {
    const query = options ? getPagingQueryString(options) : '';
    return this.get<Paging<Hub>>(`/hubs/${query}`);
  }

  getHubById(id: string) {
    return this.get<Hub>(`/hubs/${id}`);
  }

  createHub(data: CreateHubData) {
    return this.post<Hub>(`/hubs`, data);
  }

  updateHub(id: string, data: UpdateHubData) {
    return this.put(`/hubs/${id}`, data);
  }

  deleteHub(id: string) {
    return this.delete<Hub>(`/hubs/${id}`);
  }
}

const hubApi = new HubApiService();

export default hubApi;
