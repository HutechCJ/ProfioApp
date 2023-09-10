'use client';
import { useQuery } from '@tanstack/react-query';
import staffApi from './staff.service';
import { getPagingQueryString } from '@/common/utils/string';

const useGetStaffs = (options?: Partial<PagingOptions>) => {
  const queryData = useQuery(
    [`staffs/get${options ? `?${getPagingQueryString(options)}` : ''}`],
    {
      queryFn: () => staffApi.getStaffs(options),
    },
  );

  return queryData;
};
export default useGetStaffs;
