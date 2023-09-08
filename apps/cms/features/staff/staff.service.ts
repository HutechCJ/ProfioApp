'use client';

import { getPagingQueryString } from '@/common/utils/string';
import { CreateStaffRequest, Staff } from './staff.types';
import HttpService from '@/common/services/http.service';

class StaffApiService extends HttpService {
  getStaffs(options?: Partial<PagingOptions>) {
    const query = options ? getPagingQueryString(options) : '';
    return this.get<Paging<Staff>>(`/staffs/${query}`);
  }

  getStaffById(id: string) {
    return this.get<Staff>(`/staffs/${id}`);
  }

  createStaff(data: CreateStaffRequest) {
    return this.post<Staff>('/staffs', data);
  }

  updateStaff(id: string) {
    return this.put<Staff>(`/staffs/${id}`);
  }

  deleteStaff(id: string) {
    return this.delete<Staff>(`/staffs/${id}`);
  }
}

const staffApi = new StaffApiService();

export default staffApi;
