'use client';

import HttpService from '@/common/services/http.service';
import { Counter, CounterEntity } from './counter.types';

class CounterApiService extends HttpService {
  countEntities(entities: CounterEntity[]) {
    const query = getCounterEntityTypesQueryString(entities);

    return this.get<Partial<Counter>>(`/counters/entities${query}`);
  }
}

export function getCounterEntityTypesQueryString(entities: CounterEntity[]) {
  return `?${entities.map((v) => `entityTypes=${v}`).join('&')}`;
}

const counterApi = new CounterApiService();

export default counterApi;
