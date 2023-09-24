'use client';
import { useQuery } from '@tanstack/react-query';
import userApi from './user.service';
import { getPagingQueryString } from '@/common/utils/string';

const useGetUsers = (options?: Partial<PagingOptions>) => {
  const queryData = useQuery(
    [`users/get${options ? `?${getPagingQueryString(options)}` : ''}`],
    {
      queryFn: () => userApi.getUsers(options),
    },
  );

  return queryData;
};
export default useGetUsers;
