'use client';
import { useQuery } from '@tanstack/react-query';
import orderApi from './order.service';

const useGetOrderHubsPath = (id: string) => {
  const queryData = useQuery([`users/get/${id}/hubs/path`], {
    queryFn: () => orderApi.getOrderHubPathById(id),
    keepPreviousData: true,
  });

  return queryData;
};
export default useGetOrderHubsPath;
