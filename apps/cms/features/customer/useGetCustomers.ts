'use client';
import { useQuery } from '@tanstack/react-query';
import customerApi from './customer.service';
import { getPagingQueryString } from '@/common/utils/string';

const useGetCustomers = (options?: Partial<PagingOptions>) => {
  const queryData = useQuery(
    [`customers/get${options ? `?${getPagingQueryString(options)}` : ''}`],
    {
      queryFn: () => customerApi.getCustomers(options),
    }
  );

  return queryData;
};
export default useGetCustomers;
