'use client';

import HttpService from '@/common/services/http.service';
import { Example } from './example.types';
import { getPagingQueryString } from '@/common/utils/string';

class ExampleApiService extends HttpService {
  getExamples(options?: Partial<PagingOptions>) {
    const query = options ? getPagingQueryString(options) : '';
    return this.get<Paging<Example>>(`/examples/${query}`);
  }

  getExampleById(id: string) {
    return this.get<Example>(`/examples/${id}`);
  }

  createExample(data: any) {
    return this.post<Example>(`/examples`, data);
  }

  updateExample(id: string, data: any) {
    return this.put<Example>(`/examples/${id}`, data);
  }

  deleteExample(id: string) {
    return this.delete<Example>(`/examples/${id}`);
  }
}

const exampleApi = new ExampleApiService();

export default exampleApi;
