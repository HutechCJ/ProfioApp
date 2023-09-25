'use client';
import { useQuery } from '@tanstack/react-query';
import hubApi from './hub.service';

const useGetHub = (id: string) => {
  const queryData = useQuery([`users/get/${id}`], {
    queryFn: () => hubApi.getHubById(id),
    keepPreviousData: true,
  });

  return queryData;
};
export default useGetHub;
