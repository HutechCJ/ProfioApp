'use client';

import HttpService from '@/common/services/http.service';
import { CreateRouteData, Route, UpdateRouteData } from './route.types';
import { getPagingQueryString } from '@/common/utils/string';

class RouteApiService extends HttpService {
  getOrderHistories(options?: Partial<PagingOptions>) {
    const query = options ? getPagingQueryString(options) : '';
    return this.get<Paging<Route>>(`/routes/${query}`);
  }

  getRouteById(id: string) {
    return this.get<Route>(`/routes/${id}`);
  }

  createRoute(data: CreateRouteData) {
    return this.post<Route>(`/routes`, data);
  }

  updateRoute(id: string, data: UpdateRouteData) {
    return this.put(`/routes/${id}`, data);
  }

  deleteRoute(id: string) {
    return this.delete<Route>(`/routes/${id}`);
  }
}

const routeApi = new RouteApiService();

export default routeApi;
