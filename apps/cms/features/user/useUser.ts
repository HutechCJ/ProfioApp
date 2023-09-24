'use client';
import { useQuery } from '@tanstack/react-query';
import userApi from './user.service';

const useUser = () => {
  const { data, isLoading } = useQuery([`checkAuthorization`], {
    queryFn: () => userApi.checkAuthorization(),
    keepPreviousData: true,
  });

  return { data: data?.data ?? null, isLoading };
};
export default useUser;
