'use client';
import { useQuery } from '@tanstack/react-query';
import userApi from './user.service';

const useGetUser = (id: string) => {
  const queryData = useQuery([`users/get/${id}`], {
    queryFn: () => userApi.getUserById(id),
    keepPreviousData: true,
  });

  return queryData;
};
export default useGetUser;
