'use client';
import { useQuery } from '@tanstack/react-query';
import hubApi from './hub.service';
import { getPagingQueryString } from '@/common/utils/string';

const useGetHubs = (options?: Partial<PagingOptions>) => {
  const queryData = useQuery(
    [`hubs/get${options ? `?${getPagingQueryString(options)}` : ''}`],
    {
      queryFn: () => hubApi.getHubs(options),
      // keepPreviousData: true,
    },
  );

  return queryData;
};
export default useGetHubs;
