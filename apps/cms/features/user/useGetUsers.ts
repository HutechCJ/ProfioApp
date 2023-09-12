'use client';
import { useQuery } from '@tanstack/react-query';
import userApi from './user.service';

const useGetUsers = () => {
  const queryData = useQuery(['users/get'], {
    queryFn: () => userApi.getUsers(),
    keepPreviousData: true,
  });

  return queryData;
};
export default useGetUsers;
